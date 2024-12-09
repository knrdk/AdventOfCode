﻿using AoC;
using AoC.D09;

// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello. Solving Aoc exercise!");

string inputPath = @"C:\Users\knrdk\source\repos\AdventOfCode\2024\AoC\D09\input";
 
ISolver solver = new Day09(inputPath);

string part1Solution = await solver.SolvePart1();
Console.WriteLine($"Part1: {part1Solution}");

string part2Solution = await solver.SolvePart2();
Console.WriteLine($"Part2: {part2Solution}");