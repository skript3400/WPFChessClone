using WPFChessClone.Data;
using WPFChessClone.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace WPFChessClone.Logic.Core.RuleSystem
{
    public class PathGetter
    {
        public PathGetter() { }
        public List<Move> get(Board board, Field field, bool isRadius1, List<Achsis> achses)
        {
            Piece piece = field.piece;
            List<Move> moves = new List<Move>();
            int dLimit = 8;
            if (isRadius1) dLimit = 2;

            if (achses.Contains(Achsis.VERTICAL))
            {
                //dir1
                for (int dy = 1; dy < dLimit; dy++)
                {
                    int ny = field.y + dy;
                    if (ny > 7) break;
                    Field tmpF = board.getField(field.x, ny);
                    if (!tmpF.isEmpty)
                    {
                        if (tmpF.piece.color != piece.color) moves.Add(new Move(field, tmpF, piece));
                        break;
                    }
                    moves.Add(new Move(field, tmpF, piece));
                }
                //dir2
                for (int dy = -1; dy > (-1) * dLimit; dy--)
                {
                    int ny = field.y + dy;
                    if (ny < 0) break;
                    Field tmpF = board.getField(field.x, ny);
                    if (!tmpF.isEmpty)
                    {
                        if (tmpF.piece.color != piece.color) moves.Add(new Move(field, tmpF, piece));
                        break;
                    }
                    moves.Add(new Move(field, tmpF, piece));
                }
            }
            if (achses.Contains(Achsis.HORIZONTAL))
            {
                //dir1
                for (int dx = 1; dx < dLimit; dx++)
                {
                    int nx = field.x + dx;
                    if (nx > 7) break;
                    Field tmpF = board.getField(nx, field.y);
                    if (!tmpF.isEmpty)
                    {
                        if (tmpF.piece.color != piece.color) moves.Add(new Move(field, tmpF, piece));
                        break;
                    }
                    moves.Add(new Move(field, tmpF, piece));
                }
                //dir2
                for (int dx = -1; dx > -dLimit; dx--)
                {
                    int nx = field.x + dx;
                    if (nx < 0) break;
                    Field tmpF = board.getField(nx, field.y);
                    if (!tmpF.isEmpty)
                    {
                        if (tmpF.piece.color != piece.color) moves.Add(new Move(field, tmpF, piece));
                        break;
                    }
                    moves.Add(new Move(field, tmpF, piece));
                }
            }
            if (achses.Contains(Achsis.DIAGONAL))
            {

                //dir 1
                for (int d = 1; d < dLimit; d++)
                {
                    int nx = field.x + d;
                    int ny = field.y + d;
                    if (nx > 7) break;
                    if (ny > 7) break;
                    Field tmpF = board.getField(nx, ny);
                    if (!tmpF.isEmpty)
                    {
                        if (tmpF.piece.color != piece.color) moves.Add(new Move(field, tmpF, piece));
                        break;
                    }
                    moves.Add(new Move(field, tmpF, piece));
                }
                for (int d = 1; d < dLimit; d++)
                {
                    int nx = field.x - d;
                    int ny = field.y + d;
                    if (nx < 0) break;
                    if (ny > 7) break;
                    Field tmpF = board.getField(nx, ny);
                    if (!tmpF.isEmpty)
                    {
                        if (tmpF.piece.color != piece.color) moves.Add(new Move(field, tmpF, piece));
                        break;
                    }
                    moves.Add(new Move(field, tmpF, piece));
                }


                //dir 2
                for (int d = -1; d > (-1) * dLimit; d--)
                {
                    int nx = field.x + d;
                    int ny = field.y + d;
                    if (nx < 0) break;
                    if (ny < 0) break;
                    Field tmpF = board.getField(nx, ny);
                    if (!tmpF.isEmpty)
                    {
                        if (tmpF.piece.color != piece.color) moves.Add(new Move(field, tmpF, piece));
                        break;
                    }
                    moves.Add(new Move(field, tmpF, piece));
                }
                for (int d = -1; d > (-1) * dLimit; d--)
                {
                    int nx = field.x - d;
                    int ny = field.y + d;
                    if (nx > 7) break;
                    if (ny < 0) break;
                    Field tmpF = board.getField(nx, ny);
                    if (!tmpF.isEmpty)
                    {
                        if (tmpF.piece.color != piece.color) moves.Add(new Move(field, tmpF, piece));
                        break;
                    }
                    moves.Add(new Move(field, tmpF, piece));
                }

            }
            return moves;
        }
        public enum Achsis
        {
            HORIZONTAL, VERTICAL, DIAGONAL
        }
    }
}
