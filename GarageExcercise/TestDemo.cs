﻿using Castle.Core.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

[assembly: InternalsVisibleTo("GarageExercise.Tests")]
namespace GarageExercise
{
    internal class TestDemo
    {
        public int TestProp { get; set; }
    }
}