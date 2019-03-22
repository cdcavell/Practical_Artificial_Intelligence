using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PAI
{
    public class Cnf
    {
        public List<Clause> Clauses { get; set; }

        public Cnf()
        {
            Clauses = new List<Clause>();
        }

        public Cnf(And and)
        {
            Clauses = new List<Clause>();
            RemoveParenthesis(and);
        }
        public void SimplifyCnf()
        {
            Clauses.RemoveAll(TautologyClauses);
        }

        private bool TautologyClauses(Clause clause)
        {
            for (var i = 0; i < clause.Literals.Count; i++)
            {
                for (var j = i + 1;
                j < clause.Literals.Count - 1; j++)
                {
                    // Checking that literal i and literal
                    // j are not of the same type; i.e., both
                    // variables or negated literals.
                    if (!(clause.Literals[i] is Variable
                    && clause.Literals[j] is Variable) &&
                        !(clause.Literals[i] is Not && clause.
                        Literals[j] is Not))
                    {
                        var not = clause.Literals[i] is Not ? clause.Literals[i] as
                        Not : clause.Literals[j] as Not;
                        var @var = clause.Literals[i] is Variable ? clause.Literals[i]
                        as Variable : clause.Literals[j] as Variable;
                        if (IsNegation(not, @var))
                            return true;
                    }
                }
            }

            return false;
        }

        private bool IsNegation(Not f1, Variable f2)
        {
            return (f1.P as Variable).Name == f2.Name;
        }

        private void Join(IEnumerable<Clause> others)
        {
            Clauses.AddRange(others);
        }

        private void RemoveParenthesis(And and)
        {
            var currentAnd = and;

            while (true)
            {
                // If P is OR or literal and Q is OR or literal.
                if ((currentAnd.P is Or || currentAnd.P is
                Variable || currentAnd.P is Not) &&
                    (currentAnd.Q is Or || currentAnd.Q is
                    Variable || currentAnd.Q is Not))
                {
                    Clauses.Add(new Clause
                    {
                        Literals = new List<Formula>(currentAnd.P.Literals())
                    });
                    Clauses.Add(new Clause
                    {
                        Literals = new List<Formula>(currentAnd.Q.Literals())
                    });
                    break;
                }
                // If P is AND and Q is OR or literal.
                if (currentAnd.P is And && (currentAnd.Q is Or ||
                currentAnd.Q is Variable || currentAnd.Q is Not))
                {
                    Clauses.Add(new Clause
                    {
                        Literals = new List<Formula>(currentAnd.Q.Literals())
                    });
                    currentAnd = currentAnd.P as And;
                }
                // If P is OR or literal and Q is AND.
                if ((currentAnd.P is Or || currentAnd.P is
                Variable || currentAnd.P is Not) && currentAnd.
                Q is And)
                {
                    Clauses.Add(new Clause
                    {
                        Literals = new List<Formula>(currentAnd.P.Literals())
                    });
                    currentAnd = currentAnd.Q as And;
                }
                // If both P and Q are ANDs.
                if (currentAnd.P is And && currentAnd.Q is And)
                {
                    RemoveParenthesis(currentAnd.P as And);
                    RemoveParenthesis(currentAnd.Q as And);
                    break;
                }
            }
        }


        public bool Dpll()
        {
            return Dpll(new Cnf { Clauses = new List<Clause>(Clauses) });
        }

        private bool Dpll(Cnf cnf)
        {
            // The CNF with no clauses is assumed to be True
            if (cnf.Clauses.Count == 0)
                return true;

            // Rule One Literal: if there exists a clause with a single literal
            // we assign it True and remove every clause containing it.
            var cnfAfterOneLit = OneLiteral(cnf);

            if (cnfAfterOneLit.Item2 == 0)
                return true;

            if (cnfAfterOneLit.Item2 < 0)
                return false;

            cnf = cnfAfterOneLit.Item1;

            // Rule Pure Literal: if there exists a literal and its negation does not exist in any clause of Cnf
            var cnfPureLit = PureLiteralRule(cnf);

            // Rule Split: splitting occurs over a literal and creates 2 branches of the tree
            var split = Split(cnfPureLit);

            return Dpll(split.Item1) || Dpll(split.Item2);
        }

        private Tuple<Cnf, int> OneLiteral(Cnf cnf)
        {
            var unitLiteral = UnitClause(cnf);
            if (unitLiteral == null)
                return new Tuple<Cnf, int>(cnf, 1);

            var newCnf = new Cnf();
            while (unitLiteral != null)
            {
                var clausesToRemove = new List<int>();
                var i = 0;

                // 1st Loop - Finding clauses where the
                // unit literal is, these clauses will not be
                // considered in the new Cnf
                foreach (var clause in cnf.Clauses)
                {
                    if (clause.Literals.Any(literal => clause.
                    LiteralEquals(literal, unitLiteral)))
                        clausesToRemove.Add(i);
                    i++;
                }

                // New Cnf after removing every clause where
                // unit literal is
                newCnf = new Cnf();

                // 2nd Loop - Leave clause that do not include
                // the unit literal
                for (var j = 0; j < cnf.Clauses.Count; j++)
                {
                    if (!clausesToRemove.Contains(j))
                        newCnf.Clauses.Add(cnf.Clauses[j]);
                }
                // No clauses, which implies SAT
                if (newCnf.Clauses.Count == 0)
                    return new Tuple<Cnf, int>(newCnf, 0);
                // Remove negation of unit literal from
                // remaining clauses
                var unitNegated = NegateLiteral(unitLiteral);
                var clausesNoLitNeg = new List<Clause>();

                foreach (var clause in newCnf.Clauses)
                {
                    var newClause = new Clause();

                    // Leaving every literal except the unit
                    // literal negated
                    foreach (var literal in clause.Literals)
                        if (!clause.LiteralEquals(literal,
                        unitNegated))
                            newClause.Literals.Add(literal);

                    clausesNoLitNeg.Add(newClause);
                }

                newCnf.Clauses = new List<Clause>(clausesNoLitNeg);
                // Resetting variables for next stage
                cnf = newCnf;
                unitLiteral = UnitClause(cnf);
                // Empty clause found
                if (cnf.Clauses.Any(c => c.Literals.Count == 0))
                    return new Tuple<Cnf, int>(newCnf, -1);
            }

            return new Tuple<Cnf, int>(newCnf, 1);
        }

        public Formula NegateLiteral(Formula literal)
        {
            if (literal is Variable)
                return new Not(literal);
            if (literal is Not)
                return (literal as Not).P;
            return null;
        }

        private Formula UnitClause(Cnf cnf)
        {
            foreach (var clause in cnf.Clauses)
                if (clause.Literals.Count == 1)
                    return clause.Literals.First();
            return null;
        }


        private Cnf PureLiteralRule(Cnf cnf)
        {
            var pureLiterals = PureLiterals(cnf);
            if (pureLiterals.Count() == 0)
                return cnf;

            var newCnf = new Cnf();
            var clausesRemoved = new SortedSet<int>();

            // Checking what clauses contain pure literals
            foreach (var pureLiteral in pureLiterals)
            {
                for (var i = 0; i < cnf.Clauses.Count; i++)
                {
                    if (cnf.Clauses[i].Contains(pureLiteral))
                        clausesRemoved.Add(i);
                }
            }

            // Creating the new set of clauses
            for (var i = 0; i < cnf.Clauses.Count; i++)
            {
                if (!clausesRemoved.Contains(i))
                    newCnf.Clauses.Add(cnf.Clauses[i]);
            }

            return newCnf;
        }

        private IEnumerable<Formula> PureLiterals(Cnf cnf)
        {
            var result = new List<Formula>();
            foreach (var clause in cnf.Clauses)
                foreach (var literal in clause.Literals)
                {
                    if (PureLiteral(cnf, literal))
                        result.Add(literal);
                }

            return result;
        }

        private bool PureLiteral(Cnf cnf, Formula literal)
        {
            var negation = NegateLiteral(literal);

            foreach (var clause in cnf.Clauses)
            {
                foreach (var l in clause.Literals)
                    if (clause.LiteralEquals(l, negation))
                        return false;
            }

            return true;
        }

        private Tuple<Cnf, Cnf> Split(Cnf cnf)
        {
            var literal = Heuristics.ChooseLiteral(cnf);
            var tuple = SplittingOnLiteral(cnf, literal);

            return new Tuple<Cnf, Cnf>(RemoveLiteral(tuple.Item1,
            literal), RemoveLiteral(tuple.Item2,
            NegateLiteral(literal)));
        }

        private Cnf RemoveLiteral(Cnf cnf, Formula literal)
        {
            var result = new Cnf();

            foreach (var clause in cnf.Clauses)
                result.Clauses.Add(clause.RemoveLiteral(literal));

            return result;
        }

        private Tuple<Cnf, Cnf> SplittingOnLiteral(Cnf cnf, Formula literal)
        {
            // List of clauses containing literal
            var @in = new List<Clause>();
            // List of clauses containing Not(literal)
            var inNegated = new List<Clause>();
            // List of clauses not containing literal nor Not(literal)
            var @out = new List<Clause>();
            var negated = NegateLiteral(literal);

            foreach (var clause in cnf.Clauses)
            {
                if (clause.Contains(literal))
                    @in.Add(clause);
                else if (clause.Contains(negated))
                    inNegated.Add(clause);
                else
                    @out.Add(clause);
            }

            var inCnf = new Cnf { Clauses = @in };
            var outCnf = new Cnf { Clauses = @inNegated };
            inCnf.Join(@out);
            outCnf.Join(@out);

            return new Tuple<Cnf, Cnf>(inCnf, outCnf);
        }

        public override string ToString()
        {
            if (Clauses.Count > 0)
            {
                var result = "";
                foreach (var clausule in Clauses)
                {
                    var c = "";
                    foreach (var literal in clausule.Literals)
                        c += literal + ",";

                    result += "(" + c + ")";
                }
                return result;
            }

            return "Empty CNF";
        }
    }
}