using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFChessClone.Logic;
using WPFChessClone.Model;

public class Move
{
    public Field startField;
    public Field targetField;
    public Piece piece;

    public Move(Field sfIn, Field tfIn, Piece pIn)
    {
        startField = sfIn;
        targetField = tfIn;
        piece = pIn;

    }
    public string getString()
    {
        return "todo";
    }

    public static bool operator ==(Move a, Move b)
    {
        return (
        a.startField.Coordinates == b.startField.Coordinates
        &&
        a.targetField.Coordinates == b.targetField.Coordinates
        &&
        a.piece.type == b.piece.type
        );
    }
    public static bool operator !=(Move a, Move b)
    {
        if(a is null || b is null)
        {
            return !(a is null && b is null);
        }

        return (
        a.startField.Coordinates != b.startField.Coordinates
        ||
        a.targetField.Coordinates != b.targetField.Coordinates
        ||
        a.piece.type != b.piece.type
        );
    }

}
