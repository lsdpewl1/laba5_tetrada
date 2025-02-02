﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace laba1
{
    internal class Lexeme
    {
        public int Code { get; set; }
        public LexemeType Type { get; set; }
        public string Token { get; set; }
        public int StartPosition { get; set; }
        public int EndPosition { get; set; }

        public Lexeme(int code, LexemeType type, string input, int startPosition, int endPosition)
        {
            Code = code;
            Type = type;
            Token = input.Substring(startPosition, endPosition - startPosition + 1);
            StartPosition = startPosition;
            EndPosition = endPosition;
        }
    }

    public enum LexemeType
    {
        Letter,
        Plus,
        Minus,
        Mult,
        Div,
        Equal,
        Semicolon,
        SkobaOpen,
        SkobaClose,
        Invalid
    }
}