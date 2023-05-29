using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using WPFChessClone.Data;

namespace WPFChessClone.Model
{
    public class Board
    {
        public Field blackKing { get; set; }
        public bool blackChecked = false;
        public Field whiteKing { get; set; }
        public bool whiteChecked = false;

        public Field[,] fields { get; set; } = new Field[8, 8];

        public Board()
        {
            initBoard();
        }
        public Board(Board board)
        {
            foreach (Field field in board.fields)
            {
                fields[field.x, field.y] = new Field(new Coordinates(field.x, field.y), field.piece);
            }
            blackKing = fields[board.blackKing.Coordinates.x, board.blackKing.Coordinates.y];
            whiteKing = fields[board.whiteKing.Coordinates.x, board.whiteKing.Coordinates.y];
            blackChecked = board.blackChecked;
            whiteChecked = board.whiteChecked;
        }

        public void Update(Board board)
        {
            this.fields = board.fields;
        }

        public Field getField(int x, int y)
        {
            return fields[x, y];
        }
        public Field getField(Coordinates c)
        {
            return fields[c.x, c.y];
        }
        public void setField(int x, int y, Field f)
        {
            fields[x, y] = f;
        }

        private void initBoard()
        {
            //white-Side
            ChessColor color = ChessColor.White;

            fields[0, 0] = new Field(new Coordinates(0, 0), new Rook(color));
            fields[1, 0] = new Field(new Coordinates(1, 0), new Knight(color));
            fields[2, 0] = new Field(new Coordinates(2, 0), new Bishop(color));
            fields[3, 0] = new Field(new Coordinates(3, 0), new Queen(color));
            fields[4, 0] = new Field(new Coordinates(4, 0), new King(color));
            whiteKing = fields[4, 0];
            fields[5, 0] = new Field(new Coordinates(5, 0), new Bishop(color));
            fields[6, 0] = new Field(new Coordinates(6, 0), new Knight(color));
            fields[7, 0] = new Field(new Coordinates(7, 0), new Rook(color));

           
            int y = 1;
            for (int x = 0; x < 8; x++)
            {
                Piece pTemp = new Pawn(color);
                Coordinates cTemp = new Coordinates(x, y);
                fields[x, y] = new Field(cTemp, pTemp);
            }
            //free-Space
            for (y = 2; y <= 5; y++)
            {
                for (int x = 0; x < 8; x++)
                {
                    Coordinates cTemp = new Coordinates(x, y);
                    fields[x, y] = new Field(cTemp);
                }
            }
            //black-Side
            color = ChessColor.Black;
            y = 6;
            for (int x = 0; x < 8; x++)
            {
                Piece pTemp = new Pawn(color);
                Coordinates cTemp = new Coordinates(x, y);
                fields[x, y] = new Field(cTemp, pTemp);
            }

            //!! remove !!//
            fields[7, 4] = new Field(new Coordinates(7, 4), new Bishop(ChessColor.Black));
            fields[3, 2] = new Field(new Coordinates(3, 2), new Knight(ChessColor.White));
            //!! remove !!//

            fields[0, 7] = new Field(new Coordinates(0, 7), new Rook(color));
            fields[1, 7] = new Field(new Coordinates(1, 7), new Knight(color));
            fields[2, 7] = new Field(new Coordinates(2, 7), new Bishop(color));
            fields[3, 7] = new Field(new Coordinates(3, 7), new Queen(color));
            fields[4, 7] = new Field(new Coordinates(4, 7), new King(color));
            blackKing = fields[4, 7];
            fields[5, 7] = new Field(new Coordinates(5, 7), new Bishop(color));
            fields[6, 7] = new Field(new Coordinates(6, 7), new Knight(color));
            fields[7, 7] = new Field(new Coordinates(7, 7), new Rook(color));
        }


    }
}
