using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static laba1.Form1;
using System.Windows.Forms;

namespace laba1
{
    public class Parser
    {
        private List<Lexeme> lexemes;
        private int position;
        public int counter;
        public List<LexemeType> expectedLexemes;
        public List<LexemeType> foundLexemes;
        public string str;

        public Parser(List<Lexeme> lexemes)
        {
            this.lexemes = lexemes;
            this.position = 0;
            this.counter = 0;
        }

        public void Parse(DataGridView dataGridView1)
        {
            DEF(dataGridView1);
        }

        private void DEF(DataGridView dataGridView1)
        {
            try
            {

                int res = 0;
                for (int u = position; u < lexemes.Count; u++)
                {
                    if (lexemes[u].Type == LexemeType.Keyword)
                    {
                        res = 1;
                        break;
                    }
                }

                if (res == 0)
                {
                    //dataGridView1.Rows.Add($"Ошибка синтаксиса в позиции {lexemes[position].StartPosition}: ожидался ключевое слово 'const'");
                    dataGridView1.Rows.Add("Ожидалось ключевое слово 'const'", lexemes[position].StartPosition);
                    counter++;
                    DEFREM(dataGridView1);
                }
                else
                {

                    if (lexemes[position].Type == LexemeType.Keyword)
                {
                    str += lexemes[position].Token;
                    position++;
                    
                    DEFREM(dataGridView1);
                }
                else if (lexemes[position].Type == LexemeType.Invalid)
                {

                    dataGridView1.Rows.Add($"Недопустимый символ '{lexemes[position].Token}'",lexemes[position].StartPosition);

                    position++;
                    counter++;
                    DEF(dataGridView1);
                }
                else if (lexemes[position].Type != LexemeType.Keyword)
                {

                    
                        dataGridView1.Rows.Add($"Cимвол '{lexemes[position].Token}', lexemes[position].StartPosition");
                        position++;
                        counter++;
                        DEF(dataGridView1);
                    }

                    
                }
                
                //else
                //{
                //    dataGridView1.Rows.Add($"Ошибка синтаксиса в позиции {lexemes[position].StartPosition}: ожидался ключевое слово 'const'");
                //    counter++;
                //    DEFREM(dataGridView1);
                //}
                //}
            }
            catch (ArgumentOutOfRangeException)
            {
                dataGridView1.Rows.Add($"Неожиданный символ '\0'");
                counter++;
            }
        }
         
        private void DEFREM(DataGridView dataGridView1)
        {
            try
            {
                int res = 0;
                for (int u = position; u < lexemes.Count; u++)
                {
                    if (lexemes[u].Type == LexemeType.Delimiter)
                    {
                        res = 1;
                        break;
                    }
                }

                if (res == 0)
                {
                    //dataGridView1.Rows.Add($"Ошибка синтаксиса в позиции {lexemes[position].StartPosition}: ожидался пробел");
                    dataGridView1.Rows.Add("Ожидался пробел", lexemes[position].StartPosition);
                    counter++;
                    ID(dataGridView1);
                }
                else
                {

                    if (lexemes[position].Type == LexemeType.Delimiter)
                {
                    str += lexemes[position].Token;
                    position++;
                    ID(dataGridView1);
                }
                else if (lexemes[position].Type == LexemeType.Invalid)
                {

                    dataGridView1.Rows.Add($"Недопустимый символ '{lexemes[position].Token}'", lexemes[position].StartPosition);
                    counter++;
                    position++;
                    DEFREM(dataGridView1);
                }

                else if (lexemes[position].Type != LexemeType.Delimiter)
                {

                    
                        dataGridView1.Rows.Add($"Cимвол '{lexemes[position].Token}'", lexemes[position].StartPosition);
                        counter++;
                        position++;
                        DEFREM(dataGridView1);
                    }

                    
                }
                //else
                //{
                //    dataGridView1.Rows.Add($"Ошибка синтаксиса в позиции {lexemes[position].StartPosition}: ожидался пробел");
                //    counter++;
                //    ID(dataGridView1);
                //}
            }
            catch (ArgumentOutOfRangeException)
            {
                dataGridView1.Rows.Add($"Неожиданный символ '\0'");
                counter++;
            }
        }

        private void ID(DataGridView dataGridView1)
        {
            try
            {
                int res = 0;
                for (int u = position; u < lexemes.Count; u++)
                {
                    if (lexemes[u].Type == LexemeType.Identifier)
                    {
                        res = 1;
                        break;
                    }
                }


                if (res == 0)
                {
                    //dataGridView1.Rows.Add($"Ошибка синтаксиса в позиции {lexemes[position].StartPosition}: ожидался идентификатор");
                    dataGridView1.Rows.Add("Ожидался идентификатор", lexemes[position].StartPosition);
                    counter++;
                    //position++;
                    IDREM(dataGridView1);
                }
                else { 

                if (lexemes[position].Type == LexemeType.Identifier)
                {
                    str += lexemes[position].Token;
                    position++;
                    IDREM(dataGridView1);
                }
                else if (lexemes[position].Type == LexemeType.Invalid)
                {

                    dataGridView1.Rows.Add($"Недопустимый символ  '{lexemes[position].Token}'", lexemes[position].StartPosition);
                    counter++;
                    position++;
                    ID(dataGridView1);
                }
                    else if (lexemes[position].Type != LexemeType.Identifier)
                    {
                    dataGridView1.Rows.Add($"Cимвол '{lexemes[position].Token}'", lexemes[position].StartPosition);
                    counter++;
                    position++;
                    ID(dataGridView1);
                    //
                    //if (lexemes[position].Type != LexemeType.Identifier) { 
                            
                    //else
                    //{

                    //}
                        }
                    
                }

                //else if (lexemes[position].Type != LexemeType.Identifier)
                //{
                //    //dataGridView1.Rows.Add($"Отброшенный символ {lexemes[position].Token} в позиции {lexemes[position].StartPosition}");
                //    dataGridView1.Rows.Add($"Ошибка синтаксиса в позиции {lexemes[position].StartPosition}: ожидался идентификатор");

                //    counter++;
                //    //position++;
                //    IDREM(dataGridView1);
                //}
                //else
                //{
                //    dataGridView1.Rows.Add($"Ошибка синтаксиса в позиции {lexemes[position].StartPosition}: ожидался идентификатор");
                //    counter++;
                //    IDREM(dataGridView1);
                //}
            }

            catch (ArgumentOutOfRangeException)
            {
                dataGridView1.Rows.Add($"Неожиданный символ '\0'");
                counter++;
            }
        }

        private void IDREM(DataGridView dataGridView1)
        {
            try
            {

                int res = 0;
                for (int u = position; u < lexemes.Count; u++)
                {
                    if (lexemes[u].Type == LexemeType.Colon)
                    {
                        res = 1;
                        break;
                    }
                }

                if (res == 0)
                {
                    dataGridView1.Rows.Add("Ожидалось двоеточие", lexemes[position].StartPosition);
                    counter++;
                    TYPE(dataGridView1);
                }
                else
                {

                    if (lexemes[position].Type == LexemeType.Colon)
                {
                    str += lexemes[position].Token;
                    position++;
                    TYPE(dataGridView1);
                }
                else if (lexemes[position].Type == LexemeType.Invalid)
                {

                    dataGridView1.Rows.Add($"Недопустимый символ '{lexemes[position].Token}'", lexemes[position].StartPosition);
                    counter++;
                    position++;
                    IDREM(dataGridView1);
                }
                else if (lexemes[position].Type != LexemeType.Colon)
                {

                    
                        dataGridView1.Rows.Add($"Cимвол '{lexemes[position].Token}'", lexemes[position].StartPosition);
                        counter++;
                        position++;
                        IDREM(dataGridView1);
                    }


                    
                }
                //else
                //{
                //    dataGridView1.Rows.Add($"Ошибка синтаксиса в позиции {lexemes[position].StartPosition}: ожидался двоеточие");
                //    counter++;
                //    TYPE(dataGridView1);
                //}
            }
            catch (ArgumentOutOfRangeException)
            {
                dataGridView1.Rows.Add($"Неожиданный символ '\0'");
                counter++;
            }
}

        private void TYPE(DataGridView dataGridView1)
        {
            try
            {
                int res = 0;
                for (int u = position; u < lexemes.Count; u++)
                {
                    if (lexemes[u].Type == LexemeType.DataType)
                    {
                        res = 1;
                        break;
                    }
                }

                if (res == 0)
                {
                    dataGridView1.Rows.Add($"Ожидался тип данных", lexemes[position].StartPosition);
                    counter++;
                    TYPEREM(dataGridView1);
                }
                else
                {


                    if (lexemes[position].Type == LexemeType.DataType)
                {
                    str += lexemes[position].Token;
                    position++;
                    TYPEREM(dataGridView1);
                }
                else if (lexemes[position].Type == LexemeType.Invalid)
                {

                    dataGridView1.Rows.Add($"Недопустимый символ '{lexemes[position].Token}'", lexemes[position].StartPosition);
                    counter++;
                    position++;
                    TYPE(dataGridView1);
                }
                else if (lexemes[position].Type != LexemeType.DataType)
                {

                    
                        dataGridView1.Rows.Add($"Cимвол '{lexemes[position].Token}'", lexemes[position].StartPosition);
                        counter++;
                        position++;
                        TYPE(dataGridView1);
                    }

                    
                }
                //else
                //{
                //    dataGridView1.Rows.Add($"Ошибка синтаксиса в позиции {lexemes[position].StartPosition}: ожидался тип данных");
                //    counter++;
                //    TYPEREM(dataGridView1);
                //}
            }
            catch (ArgumentOutOfRangeException)
            {
                dataGridView1.Rows.Add($"Неожиданный символ '\0'");
                counter++;
            }
        }

        private void TYPEREM(DataGridView dataGridView1)
        {
            //try
            //{
            //    if (lexemes[position].Type == LexemeType.Delimiter)
            //    {
            //        position++;
                    try
                    {
                        int res = 0;
                        for (int u = position; u < lexemes.Count; u++)
                        {
                            if (lexemes[u].Type == LexemeType.Equally)
                            {
                                res = 1;
                                break;
                          }
                        }

                        if (res == 0)
                        {
                            dataGridView1.Rows.Add("Ожидалось равно", lexemes[position].StartPosition);
                            counter++;
                            EQUAL(dataGridView1);
                        }
                        else
                        {


                            if (lexemes[position].Type == LexemeType.Equally)
                            {
                                str += lexemes[position].Token;
                                position++;
                                EQUAL(dataGridView1);
                            }
                            else if (lexemes[position].Type == LexemeType.Invalid)
                            {

                                dataGridView1.Rows.Add($"Недопустимый символ '{lexemes[position].Token}'", lexemes[position].StartPosition);
                                counter++;
                                position++;
                                TYPEREM(dataGridView1);
                            }
                            else if (lexemes[position].Type != LexemeType.Equally)
                            {


                                dataGridView1.Rows.Add($"Символ '{lexemes[position].Token}'", lexemes[position].StartPosition);
                                counter++;
                                position++;
                                TYPEREM(dataGridView1);
                            }



                        }
                        //else
                        //{
                        //    dataGridView1.Rows.Add($"Ошибка синтаксиса в позиции {lexemes[position].StartPosition}: ожидался равно");
                        //    counter++;
                        //    EQUAL(dataGridView1);
                        //}
                    }
                    catch (ArgumentOutOfRangeException)
                    {
                        dataGridView1.Rows.Add($"Неожиданный символ '\0'");
                        counter++;
                    }
                //}

                //else {

                //    int res = 0;
                //    for (int u = position; u < lexemes.Count; u++)
                //    {
                //        if (lexemes[u].Type == LexemeType.Equally)
                //        {
                //            res = 1;
                //        }
                //    }

                //    if (res == 0)
                //    {
                //        dataGridView1.Rows.Add($"Ошибка синтаксиса в позиции {lexemes[position].StartPosition}: ожидался равно");
                //        counter++;
                //        EQUAL(dataGridView1);
                //    }
                //    else
                //    {

                //        if (lexemes[position].Type == LexemeType.Equally)
                //        {
                //            str += lexemes[position].Token;
                //            position++;
                //            EQUAL(dataGridView1);
                //        }
                //        else if (lexemes[position].Type == LexemeType.Invalid)
                //        {

                //            dataGridView1.Rows.Add($"Недопустимый символ {lexemes[position].Token} в позиции {lexemes[position].StartPosition}");
                //            counter++;
                //            position++;
                //            TYPEREM(dataGridView1);
                //        }
                //        else if (lexemes[position].Type != LexemeType.Equally/* && lexemes[position].Type != LexemeType.Delimiter*/)
                //        {

                //            //int res2 = 0;
                //            //for (int u = 0; u < lexemes.Count; u++)
                //            //{
                //            //    if (lexemes[u].Type == LexemeType.Equally/* && lexemes[u].Type != LexemeType.Delimiter*/)
                //            //    {
                //            //        res2 = 1;
                //            //    }
                //            //}

                //            //if (res2 == 0)
                //            //{
                //            //    dataGridView1.Rows.Add($"Ошибка синтаксиса в позиции {lexemes[position].StartPosition}: ожидался равно");
                //            //    counter++;
                //            //    EQUAL(dataGridView1);
                //            //}
                //            //else
                //            //{
                //                dataGridView1.Rows.Add($"Отброшенный символ {lexemes[position].Token} в позиции {lexemes[position].StartPosition}");
                //                counter++;
                //                position++;
                //                TYPEREM(dataGridView1);
                //            //}

                //        }
                //    }
                //}
           
                //else
                //{
                //    dataGridView1.Rows.Add($"Ошибка синтаксиса в позиции {lexemes[position].StartPosition}: ожидался равно");
                //    counter++;
                //    EQUAL(dataGridView1);
                //}

            //}
            //catch (ArgumentOutOfRangeException)
            //{
            //    dataGridView1.Rows.Add($"Неожиданный символ '\0'");
            //    counter++;
            //}

        }

        private void EQUAL(DataGridView dataGridView1)
        {
            //if (lexemes[position].Type == LexemeType.Delimiter)
            //{
            //    position++;
                if ((lexemes[position].Type == LexemeType.Plus) || (lexemes[position].Type == LexemeType.Minus))
                {
                    str += lexemes[position].Token;
                    position++;
                    NUMBER(dataGridView1);
                }
                else
                {
                    NUMBER(dataGridView1);
                }
            //}
            //else if ((lexemes[position].Type == LexemeType.Plus) || (lexemes[position].Type == LexemeType.Minus))
            //{
            //    str += lexemes[position].Token;
            //    position++;
            //    NUMBER(dataGridView1);
            //}
            //else
            //{
            //    NUMBER(dataGridView1);
            //}
        }

        private void NUMBER(DataGridView dataGridView1)
        {
            try
            {

                int res = 0;
                for (int u = position; u < lexemes.Count; u++)
                {
                    if (lexemes[u].Type == LexemeType.Number)
                    {
                        res = 1;
                        break;
                    }
                }


                if (res == 0)
                {
                    dataGridView1.Rows.Add("Ожидалось число", lexemes[position].StartPosition);
                    counter++;
                    NUMBERREM(dataGridView1);
                }
                else
                {
                    if (lexemes[position].Type == LexemeType.Number)
                {
                    str += lexemes[position].Token;
                    position++;
                    NUMBERREM(dataGridView1);
                }
                else if (lexemes[position].Type == LexemeType.Invalid)
                {

                    dataGridView1.Rows.Add($"Недопустимый символ '{lexemes[position].Token}'", lexemes[position].StartPosition);
                    counter++;
                    position++;
                    NUMBER(dataGridView1);
                }
                else if (lexemes[position].Type != LexemeType.Number)
                {


                    
                        dataGridView1.Rows.Add($"Символ '{lexemes[position].Token}'", lexemes[position].StartPosition);
                        counter++;
                        position++;
                        NUMBER(dataGridView1);
                    }


                    
                }
                //else
                //{
                //    dataGridView1.Rows.Add($"Ошибка синтаксиса в позиции {lexemes[position].StartPosition}: ожидалось число");
                //    counter++;
                //    NUMBERREM(dataGridView1);
                //}
            }
            catch (ArgumentOutOfRangeException)
            {
                dataGridView1.Rows.Add($"Неожиданный символ '\0'");
                counter++;
            }
        }

        private void NUMBERREM(DataGridView dataGridView1)
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
                    dataGridView1.Rows.Add($"Ожидалась точка с запятой", lexemes[position].StartPosition);
                    counter++;
                    END(dataGridView1);//?
                }
                else
                {

                    if (lexemes[position].Type == LexemeType.Semicolon)
                {
                    str += lexemes[position].Token;
                    position++;
                    END(dataGridView1);

                }
                else if (lexemes[position].Type == LexemeType.Invalid)
                {

                    dataGridView1.Rows.Add($"Недопустимый символ '{lexemes[position].Token}'", lexemes[position].StartPosition);
                    counter++;
                    position++;
                    NUMBERREM(dataGridView1);
                }
                else if (lexemes[position].Type != LexemeType.Semicolon)
                {


                    
                        dataGridView1.Rows.Add($"Cимвол '{lexemes[position].Token}'", lexemes[position].StartPosition);                       
                        counter++;
                        position++;
                        NUMBERREM(dataGridView1);
                    }


                }
                //else
                //{
                //    dataGridView1.Rows.Add($"Ошибка синтаксиса в позиции {lexemes[position].StartPosition}: ожидалась точка с запятой");
                //    counter++;
                //}
            }
            catch (ArgumentOutOfRangeException)
            {
                dataGridView1.Rows.Add($"Неожиданный символ '\0'");
                counter++;
            }

        }

        private void END(DataGridView dataGridView1)
        {
            try
            {
                if (lexemes[position].Type == LexemeType.Invalid)
                {

                    dataGridView1.Rows.Add($"Недопустимый символ '{lexemes[position].Token}'", lexemes[position].StartPosition);
                    counter++;
                    position++;
                    /*NUMBERREM*/END(dataGridView1);
                }
                else if (lexemes[position].Type == LexemeType.EndStr)
                {   
                    position++;
                    if (lexemes[position].Type == LexemeType.NewStr)
                    {
                        str += lexemes[position].Token;
                        position++;
                        DEF(dataGridView1);
                    }
                }
                else if (lexemes[position].Type != LexemeType.Invalid)
                {
                    dataGridView1.Rows.Add($"Cимвол '{lexemes[position].Token}'", lexemes[position].StartPosition);
                    counter++;
                    position++;
                    /*NUMBERREM*/END(dataGridView1);
                }
                
            }
            catch (ArgumentOutOfRangeException)
            {
            }

        }
    }
}
