﻿using OssAwesomenessTabulator.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OssAwesomenessTabulator
{
    /// <summary>
    ///  Awesomeness was inspired by Twitter's Hotness algo but uses more Pi because
    ///  Pi is always awesome.
    ///
    ///    https://github.com/twitter/twitter.github.com/blob/289a70a500a478cd039bb9994a48e58cffe2bc61/index.html#L76-L85
    ///
    ///  But really just an excuse for me to use some of the maths I did in my degree
    ///  with a bit of exponential decay and an sprinkling of irrational numbers
    /// </summary>
    public static class Awesomeness
    {

        public static readonly double Push_Awesomeness = 1000;
        public static readonly double Push_Halflife = 42 * Math.E * Math.Pow(10, -15);
        public static readonly double Star_Awesomeness = (10 + Math.PI) * Math.Pow(10, 13);

        public static int Calculate(Project project)
        {
            if (project.CommitLast == null)
            {
                // Project hasn't done anything yet, not very awesome
                return -1;
            }

            double pushTicks = DateTimeOffset.Now.Subtract((DateTimeOffset)project.CommitLast).Ticks;
            double createdTicks = DateTimeOffset.Now.Subtract(project.Created).Ticks;

            double awesomeness = (Star_Awesomeness * project.Stars) / createdTicks;
            awesomeness += Push_Awesomeness * Math.Pow(Math.E, -1 * Push_Halflife * pushTicks);

            if (awesomeness > Int16.MaxValue)
            {
                // Nobody likes a show-off.
                awesomeness = Int16.MaxValue;
            }

            return (int)awesomeness;
        }
    }
}