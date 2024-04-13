using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.XPath;

namespace laba1
{
    internal class Parser
    {
        private List<Lexeme> lexemes;
        public List<strTable> table;
        private int position;
        public int counter;
        public int numstr = 0;
        public int nameTemp = 0;

        public int countSkobaOpen = 0;
        public int countSkobaClose = 0;
        //public int flag;
        //public List<LexemeType> expectedLexemes;
        //public List<LexemeType> foundLexemes;
        public string str;

        public Parser(List<Lexeme> lexemes)
        {
            this.lexemes = lexemes;
            this.position = 0;
            this.counter = 0;
            table = new List<strTable>();
            //flag = lexemes.Count;
        }

        public void Parse(DataGridView dataGridView1)
        {
            START(dataGridView1);
        }

        private void START(DataGridView dataGridView1)
        {
            try
            {

                int res = 0;

                for (int u = position; u < lexemes.Count; u++)
                {
                    if (lexemes[u].Type == LexemeType.Letter)
                    {
                        res = 1;
                        break;
                    }

                }

                if (res == 0)
                {
                    //dataGridView1.Rows.Add($"Ошибка синтаксиса в позиции {lexemes[position].StartPosition}: ожидался ключевое слово 'const'");
                    dataGridView1.Rows.Add("Ожидалась буква", lexemes[position].StartPosition);
                    counter++;
                    EQUAL(dataGridView1);
                }
                else
                {


                    if (lexemes[position].Type == LexemeType.Letter)
                    {

                        if (lexemes[position].Token.Length > 1)
                        {
                            dataGridView1.Rows.Add($"Неправильное название аргумента '{lexemes[position].Token}'", lexemes[position].StartPosition);
                            position++;
                            counter++;
                            EQUAL(dataGridView1);
                        }
                        else
                        {
                            str += lexemes[position].Token;
                            position++;

                            EQUAL(dataGridView1);
                        }


                    }
                    else if (lexemes[position].Type == LexemeType.Invalid)
                    {

                        dataGridView1.Rows.Add($"Недопустимый символ '{lexemes[position].Token}'", lexemes[position].StartPosition);

                        position++;
                        counter++;
                        START(dataGridView1);
                    }
                    else if (lexemes[position].Type != LexemeType.Letter)
                    {
                        dataGridView1.Rows.Add($"Cимвол '{lexemes[position].Token}'", lexemes[position].StartPosition);
                        position++;
                        counter++;
                        START(dataGridView1);
                    }

                }
            }
            catch (ArgumentOutOfRangeException)
            {
                dataGridView1.Rows.Add($"Упс... Кажется чего-то не хватает...");
                counter++;
            }

        }

        private void EQUAL(DataGridView dataGridView1)
        {

            try
            {

                int res = 0;

                for (int u = position; u < lexemes.Count; u++)
                {
                    if (lexemes[u].Type == LexemeType.Equal)
                    {
                        res = 1;
                        break;
                    }

                }

                if (res == 0)
                {
                    //dataGridView1.Rows.Add($"Ошибка синтаксиса в позиции {lexemes[position].StartPosition}: ожидался ключевое слово 'const'");
                    dataGridView1.Rows.Add("Ожидалось равно", lexemes[position].StartPosition);
                    counter++;
                    NUM(dataGridView1);
                }
                else
                {


                    if (lexemes[position].Type == LexemeType.Equal)
                    {
                        str += lexemes[position].Token;
                        position++;

                        NUM(dataGridView1);
                    }
                    else if (lexemes[position].Type == LexemeType.Invalid)
                    {

                        dataGridView1.Rows.Add($"Недопустимый символ '{lexemes[position].Token}'", lexemes[position].StartPosition);

                        position++;
                        counter++;
                        EQUAL(dataGridView1);
                    }
                    else if (lexemes[position].Type != LexemeType.Equal)
                    {
                        dataGridView1.Rows.Add($"Cимвол '{lexemes[position].Token}'", lexemes[position].StartPosition);
                        position++;
                        counter++;
                        EQUAL(dataGridView1);
                    }

                }
            }
            catch (ArgumentOutOfRangeException)
            {
                dataGridView1.Rows.Add($"Упс... Кажется чего-то не хватает...");
                counter++;
            }

        }

        private void NUM(DataGridView dataGridView1)
        {
            try
            {
                while (lexemes[position].Type == LexemeType.SkobaOpen)
                {
                    position++;
                    countSkobaOpen++;
                }

                if (lexemes[position].Type == LexemeType.Minus)
                {
                    str += lexemes[position].Token;
                    position++;
                    NUMBER(dataGridView1);
                }
                else
                {
                    NUMBER(dataGridView1);
                }

            }
            catch (ArgumentOutOfRangeException)
            {
                dataGridView1.Rows.Add($"Упс... Кажется чего-то не хватает...");
                counter++;
            }

        }

        private void NUMBER(DataGridView dataGridView1)
        {
            try
            {

                int res = 0;

                for (int u = position; u < lexemes.Count; u++)
                {
                    if (lexemes[u].Type == LexemeType.Letter)
                    {
                        res = 1;
                        break;
                    }

                }

                if (res == 0)
                {
                    //dataGridView1.Rows.Add($"Ошибка синтаксиса в позиции {lexemes[position].StartPosition}: ожидался ключевое слово 'const'");
                    dataGridView1.Rows.Add("Ожидалась буква", lexemes[position].StartPosition);
                    counter++;
                    SIGN(dataGridView1);
                }
                else
                {


                    if (lexemes[position].Type == LexemeType.Letter)
                    {


                        if (lexemes[position].Token.Length > 1)
                        {
                            dataGridView1.Rows.Add($"Неправильное название аргумента '{lexemes[position].Token}'", lexemes[position].StartPosition);
                            position++;
                            counter++;
                            SIGN(dataGridView1);
                        }
                        else
                        {
                            str += lexemes[position].Token;
                            position++;

                            SIGN(dataGridView1);
                        }
                    }
                    else if (lexemes[position].Type == LexemeType.Invalid)
                    {

                        dataGridView1.Rows.Add($"Недопустимый символ '{lexemes[position].Token}'", lexemes[position].StartPosition);

                        position++;
                        counter++;
                        NUMBER(dataGridView1);
                    }
                    else if (lexemes[position].Type != LexemeType.Letter)
                    {
                        dataGridView1.Rows.Add($"Cимвол '{lexemes[position].Token}'", lexemes[position].StartPosition);
                        position++;
                        counter++;
                        NUMBER(dataGridView1);
                    }

                }
            }
            catch (ArgumentOutOfRangeException)
            {
                dataGridView1.Rows.Add($"Упс... Кажется чего-то не хватает...");
                counter++;
            }

        }

        private void SIGN(DataGridView dataGridView1)
        {
            try
            {
                while (lexemes[position].Type == LexemeType.SkobaClose)
                {
                    position++;
                    countSkobaClose++;
                }

                int res = 0;

                for (int u = position; u < lexemes.Count; u++)
                {
                    if (lexemes[u].Type == LexemeType.Plus || lexemes[u].Type == LexemeType.Minus || lexemes[u].Type == LexemeType.Mult || lexemes[u].Type == LexemeType.Div)
                    {
                        res = 1;
                        break;
                    }

                }

                if (res == 0)
                {
                    //dataGridView1.Rows.Add($"Ошибка синтаксиса в позиции {lexemes[position].StartPosition}: ожидался ключевое слово 'const'");
                    //dataGridView1.Rows.Add("Ожидалась буква", lexemes[position].StartPosition);
                    //counter++;
                    END(dataGridView1);
                }
                else
                {

                    if ((lexemes[position].Type == LexemeType.Plus) || (lexemes[position].Type == LexemeType.Minus) || (lexemes[position].Type == LexemeType.Mult) || (lexemes[position].Type == LexemeType.Div))
                    {
                        str += lexemes[position].Token;
                        position++;
                        NUM(dataGridView1);
                    }
                    else if (lexemes[position].Type == LexemeType.Invalid)
                    {
                        dataGridView1.Rows.Add($"Недопустимый символ '{lexemes[position].Token}'", lexemes[position].StartPosition);

                        position++;
                        counter++;
                        SIGN(dataGridView1);
                    }
                    else if (lexemes[position].Type != LexemeType.Plus && lexemes[position].Type != LexemeType.Minus && lexemes[position].Type != LexemeType.Mult && lexemes[position].Type != LexemeType.Div)
                    {
                        dataGridView1.Rows.Add($"Cимвол '{lexemes[position].Token}'", lexemes[position].StartPosition);
                        position++;
                        counter++;
                        SIGN(dataGridView1);
                    }

                }
            }
            catch (ArgumentOutOfRangeException)
            {
                dataGridView1.Rows.Add($"Упс... Кажется чего-то не хватает...");
                counter++;
            }

        }

        private void END(DataGridView dataGridView1)
        {
            try
            {

                int res = 0;

                for (int u = position; u < lexemes.Count; u++)
                {
                    if (lexemes[u].Type == LexemeType.Semicolon)
                    {
                        res = 1;
                        break;
                    }

                }

                if (res == 0)
                {
                    //dataGridView1.Rows.Add($"Ошибка синтаксиса в позиции {lexemes[position].StartPosition}: ожидался ключевое слово 'const'");
                    dataGridView1.Rows.Add("Ожидалась точка с запятой", lexemes[position].StartPosition);
                    counter++;
                    FINISH(dataGridView1);
                }
                else
                {

                    if (lexemes[position].Type == LexemeType.Semicolon)
                    {
                        str += lexemes[position].Token;
                        position++;

                        FINISH(dataGridView1);
                    }
                    else if (lexemes[position].Type == LexemeType.Invalid)
                    {

                        dataGridView1.Rows.Add($"Недопустимый символ '{lexemes[position].Token}'", lexemes[position].StartPosition);

                        position++;
                        counter++;
                        END(dataGridView1);
                    }
                    else if (lexemes[position].Type != LexemeType.Semicolon)
                    {
                        dataGridView1.Rows.Add($"Cимвол '{lexemes[position].Token}'", lexemes[position].StartPosition);
                        position++;
                        counter++;
                        END(dataGridView1);
                    }
                }
            }
            catch (ArgumentOutOfRangeException)
            {
                dataGridView1.Rows.Add($"Упс... Кажется чего-то не хватает...");
                counter++;
            }

        }

        public void FINISH(DataGridView dataGridView1)
        {
            try
            {


                if (lexemes[position].Type == LexemeType.Invalid)
                {

                    dataGridView1.Rows.Add($"Недопустимый символ '{lexemes[position].Token}'", lexemes[position].StartPosition);
                    counter++;
                    position++;
                    FINISH(dataGridView1);
                }
                else if (lexemes[position].Type != LexemeType.Invalid)
                {
                    dataGridView1.Rows.Add($"Cимвол '{lexemes[position].Token}'", lexemes[position].StartPosition);
                    counter++;
                    position++;
                    FINISH(dataGridView1);
                }



            }
            catch (ArgumentOutOfRangeException)
            {

            }
        }

        public void ifnoskobas(DataGridView dataGridView3)
        {
            for (int pos = 0; pos < lexemes.Count; pos++)
            {
                int res = 0;

                for (int u = 0; u < lexemes.Count; u++)
                {
                    if (lexemes[u].Type == LexemeType.Mult || lexemes[u].Type == LexemeType.Div)
                    {
                        res = 1;
                        break;
                    }

                }

                if (res == 0)
                {
                    int res2 = 0;

                    for (int u = 0; u < lexemes.Count; u++)
                    {
                        if (lexemes[u].Type == LexemeType.Plus || lexemes[u].Type == LexemeType.Minus)
                        {
                            res2 = 1;
                            break;
                        }

                    }

                    if (res2 == 0)
                    {
                        if (lexemes[pos].Type == LexemeType.Equal)
                        {
                            table.Add(new strTable());

                            table[numstr].num = numstr;
                            table[numstr].op = lexemes[pos].Token; //*
                            table[numstr].arg1 = lexemes[pos + 1].Token;//letter
                            table[numstr].result = lexemes[pos - 1].Token;
                            lexemes.RemoveAt(pos + 1);
                            lexemes.RemoveAt(pos - 1);
                            numstr++;
                            tetrada(dataGridView3);
                            return;
                            //return;
                        }
                    }
                    else
                    {
                        if ((lexemes[pos].Type == LexemeType.Plus) || (lexemes[pos].Type == LexemeType.Minus))
                        {
                            if (lexemes[pos - 1].Type == LexemeType.Letter)
                            {
                                if ((lexemes[pos - 2].Type == LexemeType.Minus) && (lexemes[pos - 3].Type != LexemeType.Letter))//знак минус относится к букве
                                {
                                    //записываем -буква в новую лексему
                                    table.Add(new strTable());

                                    table[numstr].num = numstr;
                                    table[numstr].op = lexemes[pos - 2].Token; //-
                                    table[numstr].arg1 = lexemes[pos - 1].Token; //letter
                                    table[numstr].result = $"t{nameTemp}";
                                    lexemes[pos - 2].Token = $"t{nameTemp}";
                                    nameTemp++;
                                    lexemes[pos - 2].Type = LexemeType.Letter;
                                    lexemes.RemoveAt(pos - 1);
                                    numstr++;
                                    tetrada(dataGridView3);
                                    return;

                                }
                                else // если знак минуса не относится к букве, то проверяем букву справа от равно b*(-c или c)
                                {
                                    if (lexemes[pos + 1].Type == LexemeType.Minus)
                                    {
                                        table.Add(new strTable());

                                        table[numstr].num = numstr;
                                        table[numstr].op = lexemes[pos + 1].Token; //minus
                                        table[numstr].arg1 = lexemes[pos + 2].Token; //letter
                                        table[numstr].result = $"t{nameTemp}";
                                        lexemes[pos + 1].Token = $"t{nameTemp}";
                                        nameTemp++;
                                        lexemes[pos + 1].Type = LexemeType.Letter;
                                        lexemes.RemoveAt(pos + 2);
                                        numstr++;
                                        tetrada(dataGridView3);
                                        return;
                                    }
                                    else //b*c
                                    {
                                        table.Add(new strTable());

                                        table[numstr].num = numstr;
                                        table[numstr].op = lexemes[pos].Token; //*
                                        table[numstr].arg1 = lexemes[pos - 1].Token;//letter
                                        table[numstr].arg2 = lexemes[pos + 1].Token;//letter
                                        table[numstr].result = $"t{nameTemp}";
                                        lexemes[pos].Token = $"t{nameTemp}";
                                        nameTemp++;
                                        lexemes[pos].Type = LexemeType.Letter;
                                        lexemes.RemoveAt(pos + 1);
                                        lexemes.RemoveAt(pos - 1);
                                        numstr++;
                                        tetrada(dataGridView3);
                                        return;
                                    }
                                }
                            }
                            else
                            {
                                if (lexemes[pos - 1].Type == LexemeType.Equal)
                                {
                                    table.Add(new strTable());

                                    table[numstr].num = numstr;
                                    table[numstr].op = lexemes[pos].Token; //-
                                    table[numstr].arg1 = lexemes[pos + 1].Token; //letter
                                    table[numstr].result = $"t{nameTemp}";
                                    lexemes[pos].Token = $"t{nameTemp}";
                                    nameTemp++;
                                    lexemes[pos].Type = LexemeType.Letter;
                                    lexemes.RemoveAt(pos + 1);
                                    numstr++;
                                    tetrada(dataGridView3);
                                    return;
                                }
                                //else
                                //{
                                //    richTextBox1.Text += "error";
                                //}
                            }
                        }
                    }

                }
                else
                {

                    if ((lexemes[pos].Type == LexemeType.Mult) || (lexemes[pos].Type == LexemeType.Div))
                    {
                        if (lexemes[pos - 1].Type == LexemeType.Letter)
                        {
                            if ((lexemes[pos - 2].Type == LexemeType.Minus) && (lexemes[pos - 3].Type != LexemeType.Letter))//знак минус относится к букве
                            {
                                //записываем -буква в новую лексему
                                table.Add(new strTable());

                                table[numstr].num = numstr;
                                table[numstr].op = lexemes[pos - 2].Token; //-
                                table[numstr].arg1 = lexemes[pos - 1].Token; //letter
                                table[numstr].result = $"t{nameTemp}";
                                lexemes[pos - 2].Token = $"t{nameTemp}";
                                nameTemp++;
                                lexemes[pos - 2].Type = LexemeType.Letter;
                                lexemes.RemoveAt(pos - 1);
                                numstr++;
                                tetrada(dataGridView3);
                                return;

                            }
                            else // если знак минуса не относится к букве, то проверяем букву справа от равно b*(-c или c)
                            {
                                if (lexemes[pos + 1].Type == LexemeType.Minus)
                                {
                                    table.Add(new strTable());

                                    table[numstr].num = numstr;
                                    table[numstr].op = lexemes[pos + 1].Token; //minus
                                    table[numstr].arg1 = lexemes[pos + 2].Token; //letter
                                    table[numstr].result = $"t{nameTemp}";
                                    lexemes[pos + 1].Token = $"t{nameTemp}";
                                    nameTemp++;
                                    lexemes[pos + 1].Type = LexemeType.Letter;
                                    lexemes.RemoveAt(pos + 2);
                                    numstr++;
                                    tetrada(dataGridView3);
                                    return;
                                }
                                else //b*c
                                {
                                    table.Add(new strTable());

                                    table[numstr].num = numstr;
                                    table[numstr].op = lexemes[pos].Token; //*
                                    table[numstr].arg1 = lexemes[pos - 1].Token;//letter
                                    table[numstr].arg2 = lexemes[pos + 1].Token;//letter
                                    table[numstr].result = $"t{nameTemp}";
                                    lexemes[pos].Token = $"t{nameTemp}";
                                    nameTemp++;
                                    lexemes[pos].Type = LexemeType.Letter;
                                    lexemes.RemoveAt(pos + 1);
                                    lexemes.RemoveAt(pos - 1);
                                    numstr++;
                                    tetrada(dataGridView3);
                                    return;
                                }
                            }
                        }
                    }
                }
            }
        }

        public void tetrada(DataGridView dataGridView3)
        {
            //a = b * -c + b * c;
            //for (int pos = 0; pos < lexemes.Count; pos++)
            //{
            //    if (lexemes[pos].Type == LexemeType.SkobaOpen || lexemes[pos].Type == LexemeType.SkobaClose)
            //    {
            //        lexemes.RemoveAt(pos);
            //        tetrada(richTextBox1);
            //    }
            //}           

            int posOpen = 0;
            int posClose = lexemes.Count;

            while (lexemes.Count > 2)
            {
                //posClose = lexemes.Count;


                if (countSkobaOpen != 0)
                {

                    for (int u = 0; u < lexemes.Count; u++)
                    {
                        if (lexemes[u].Type == LexemeType.SkobaClose)
                        {
                            posClose = u;
                            break;
                        }
                    }

                    for (int u = posClose; u > 0; u--)
                    {
                        if (lexemes[u].Type == LexemeType.SkobaOpen)
                        {
                            posOpen = u;
                            break;
                        }
                    }

                    if (posOpen + 2 == posClose)
                    {
                        lexemes.RemoveAt(posOpen + 2);
                        lexemes.RemoveAt(posOpen);
                        countSkobaOpen--;
                        tetrada(dataGridView3);
                        return;
                    }
                    else if (posOpen + 3 == posClose)
                    {
                        table.Add(new strTable());

                        table[numstr].num = numstr;
                        table[numstr].op = lexemes[posOpen + 1].Token; //-
                        table[numstr].arg1 = lexemes[posOpen + 2].Token; //letter
                        table[numstr].result = $"t{nameTemp}";
                        lexemes[posOpen + 1].Token = $"t{nameTemp}";
                        nameTemp++;
                        lexemes[posOpen + 1].Type = LexemeType.Letter;
                        lexemes.RemoveAt(posOpen + 2);
                        numstr++;
                        tetrada(dataGridView3);
                        return;
                    }


                    for (int pos = posOpen + 1; pos < posClose; pos++)
                    {
                        int res = 0;

                        for (int u = posOpen + 1; u < posClose; u++)
                        {
                            if (lexemes[u].Type == LexemeType.Mult || lexemes[u].Type == LexemeType.Div)
                            {
                                res = 1;
                                break;
                            }

                        }

                        if (res == 0)
                        {
                            int res2 = 0;

                            for (int u = posOpen + 1; u < posClose; u++)
                            {
                                if (lexemes[u].Type == LexemeType.Plus || lexemes[u].Type == LexemeType.Minus)
                                {
                                    res2 = 1;
                                    break;
                                }

                            }

                            if (res2 == 0)
                            {
                                if (lexemes[pos].Type == LexemeType.Equal)
                                {
                                    table.Add(new strTable());

                                    table[numstr].num = numstr;
                                    table[numstr].op = lexemes[pos].Token; //*
                                    table[numstr].arg1 = lexemes[pos + 1].Token;//letter
                                    table[numstr].result = lexemes[pos - 1].Token;
                                    lexemes.RemoveAt(pos + 1);
                                    lexemes.RemoveAt(pos - 1);
                                    numstr++;
                                    tetrada(dataGridView3);
                                    return;
                                    //break;
                                    //return;
                                }
                            }
                            else
                            {
                                if ((lexemes[pos].Type == LexemeType.Plus) || (lexemes[pos].Type == LexemeType.Minus))
                                {
                                    if (lexemes[pos - 1].Type == LexemeType.Letter)
                                    {
                                        if ((lexemes[pos - 2].Type == LexemeType.Minus) && (lexemes[pos - 3].Type != LexemeType.Letter))//знак минус относится к букве
                                        {
                                            //записываем -буква в новую лексему
                                            table.Add(new strTable());

                                            table[numstr].num = numstr;
                                            table[numstr].op = lexemes[pos - 2].Token; //-
                                            table[numstr].arg1 = lexemes[pos - 1].Token; //letter
                                            table[numstr].result = $"t{nameTemp}";
                                            lexemes[pos - 2].Token = $"t{nameTemp}";
                                            nameTemp++;
                                            lexemes[pos - 2].Type = LexemeType.Letter;
                                            lexemes.RemoveAt(pos - 1);
                                            numstr++;
                                            tetrada(dataGridView3);
                                            return;
                                        }
                                        else // если знак минуса не относится к букве, то проверяем букву справа от равно b*(-c или c)
                                        {
                                            if (lexemes[pos + 1].Type == LexemeType.Minus)
                                            {
                                                table.Add(new strTable());

                                                table[numstr].num = numstr;
                                                table[numstr].op = lexemes[pos + 1].Token; //minus
                                                table[numstr].arg1 = lexemes[pos + 2].Token; //letter
                                                table[numstr].result = $"t{nameTemp}";
                                                lexemes[pos + 1].Token = $"t{nameTemp}";
                                                nameTemp++;
                                                lexemes[pos + 1].Type = LexemeType.Letter;
                                                lexemes.RemoveAt(pos + 2);
                                                numstr++;
                                                tetrada(dataGridView3);
                                                return;
                                            }
                                            else //b*c
                                            {
                                                table.Add(new strTable());

                                                table[numstr].num = numstr;
                                                table[numstr].op = lexemes[pos].Token; //*
                                                table[numstr].arg1 = lexemes[pos - 1].Token;//letter
                                                table[numstr].arg2 = lexemes[pos + 1].Token;//letter
                                                table[numstr].result = $"t{nameTemp}";
                                                lexemes[pos].Token = $"t{nameTemp}";
                                                nameTemp++;
                                                lexemes[pos].Type = LexemeType.Letter;
                                                lexemes.RemoveAt(pos + 1);
                                                lexemes.RemoveAt(pos - 1);
                                                numstr++;
                                                tetrada(dataGridView3);
                                                return;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (lexemes[pos - 1].Type == LexemeType.Equal)
                                        {
                                            table.Add(new strTable());

                                            table[numstr].num = numstr;
                                            table[numstr].op = lexemes[pos].Token; //-
                                            table[numstr].arg1 = lexemes[pos + 1].Token; //letter
                                            table[numstr].result = $"t{nameTemp}";
                                            lexemes[pos].Token = $"t{nameTemp}";
                                            nameTemp++;
                                            lexemes[pos].Type = LexemeType.Letter;
                                            lexemes.RemoveAt(pos + 1);
                                            numstr++;
                                            tetrada(dataGridView3);
                                            return;
                                        }
                                        //else
                                        //{
                                        //    richTextBox1.Text += "error";
                                        //}
                                    }
                                }
                            }

                        }
                        else
                        {

                            if ((lexemes[pos].Type == LexemeType.Mult) || (lexemes[pos].Type == LexemeType.Div))
                            {
                                if (lexemes[pos - 1].Type == LexemeType.Letter)
                                {
                                    if ((lexemes[pos - 2].Type == LexemeType.Minus) && (lexemes[pos - 3].Type != LexemeType.Letter))//знак минус относится к букве
                                    {
                                        //записываем -буква в новую лексему
                                        table.Add(new strTable());

                                        table[numstr].num = numstr;
                                        table[numstr].op = lexemes[pos - 2].Token; //-
                                        table[numstr].arg1 = lexemes[pos - 1].Token; //letter
                                        table[numstr].result = $"t{nameTemp}";
                                        lexemes[pos - 2].Token = $"t{nameTemp}";
                                        nameTemp++;
                                        lexemes[pos - 2].Type = LexemeType.Letter;
                                        lexemes.RemoveAt(pos - 1);
                                        numstr++;
                                        tetrada(dataGridView3);
                                        return;
                                    }
                                    else // если знак минуса не относится к букве, то проверяем букву справа от равно b*(-c или c)
                                    {
                                        if (lexemes[pos + 1].Type == LexemeType.Minus)
                                        {
                                            table.Add(new strTable());

                                            table[numstr].num = numstr;
                                            table[numstr].op = lexemes[pos + 1].Token; //minus
                                            table[numstr].arg1 = lexemes[pos + 2].Token; //letter
                                            table[numstr].result = $"t{nameTemp}";
                                            lexemes[pos + 1].Token = $"t{nameTemp}";
                                            nameTemp++;
                                            lexemes[pos + 1].Type = LexemeType.Letter;
                                            lexemes.RemoveAt(pos + 2);
                                            numstr++;
                                            tetrada(dataGridView3);
                                            return;
                                        }
                                        else //b*c
                                        {
                                            table.Add(new strTable());

                                            table[numstr].num = numstr;
                                            table[numstr].op = lexemes[pos].Token; //*
                                            table[numstr].arg1 = lexemes[pos - 1].Token;//letter
                                            table[numstr].arg2 = lexemes[pos + 1].Token;//letter
                                            table[numstr].result = $"t{nameTemp}";
                                            lexemes[pos].Token = $"t{nameTemp}";
                                            nameTemp++;
                                            lexemes[pos].Type = LexemeType.Letter;
                                            lexemes.RemoveAt(pos + 1);
                                            lexemes.RemoveAt(pos - 1);
                                            numstr++;
                                            tetrada(dataGridView3);
                                            return;
                                        }
                                    }
                                }
                            }
                        }
                    }


                }
                else
                {
                    ifnoskobas(dataGridView3);
                }




            }

            //richTextBox1.Text = "#\top\targ1\targ2\tresult\n";

            
           // return;

            //flag = 1;

        }

    }
}
