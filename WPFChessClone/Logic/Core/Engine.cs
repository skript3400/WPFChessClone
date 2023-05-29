using WPFChessClone;
using WPFChessClone.Logic;
using WPFChessClone.Logic;
using WPFChessClone.Logic.Core;
using System;
using System.Diagnostics;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFChessClone.Model;
using WPFChessClone.Data;
using WPFChessClone.Logic.Core.RuleSystem;
using System.Net.Http;
namespace WPFChessClone.Logic
{
    public class Engine
    {
        protected Board _board;
        private bool _waitForPromotion = false;
        private ChessColor _playingColor = ChessColor.White;
        private List<Move> _currAllowedMoves = new List<Move>();
        private Move _lastMove;
        public Engine(Board bIn)
        {
            _board = bIn;
        }

        public void Begin()
        {
            if (_board == null)
            {
                _board = new Board();
            }
            onEngineEvent(this, new EngineEventData(EngineEventType.INIT_BOARD, _board, _lastMove));
        }

        public event EventHandler<EngineEventData> EngineEvent;

        private void onEngineEvent(object sender, EngineEventData e)
        {
            EventHandler<EngineEventData> handler = EngineEvent;
            handler?.Invoke(sender, e);
        }

        public void engineQueryHandler(object sender, EngineQueryEventArgs args)
        {
            switch (args.type)
            {
                case QueryType.GET_POSSIBLE_MOVES:
                    GetPossibleMoves(args.field);
                    break;
                case QueryType.QUERY_DONE:
                    executePromotion(args.pieceType);
                    break;
                case QueryType.MAKE_MOVE:
                    ExecuteMove(args.move);
                    break;
                default:
                    throw new ArgumentException("Engine Command not valid");
            }
        }

        private void GetPossibleMoves(Field field)
        {
            if (field.piece.color != _playingColor) return;
            List<Move> moves = new List<Move>();
            List<Field> resultFields = new List<Field>();

            RuleSet ruleSet = Utils.getRuleSet(field.piece,_board);
            moves = ruleSet.getMoves(field,true);

            Castle.currCastleMoves = Castle.getMoves(_board, _playingColor, field.piece);
            Castle.currCastleMoves.ForEach((Move m) => { moves.Add(m); });          

            _currAllowedMoves = moves;

            foreach (Move move in moves)
            {
                resultFields.Add(move.targetField);
            }

            EngineEventData eventData = new EngineEventData(EngineEventType.POSSIBLE_MOVE, resultFields);
            onEngineEvent(this, eventData);
        }

        
        private void ExecuteMove (Move move)
        {
            if (_waitForPromotion) return;
            if(Promotion.canPromote(_board, move))
            {
                Utils.move(_board, move, false);
                _waitForPromotion = true;
                Promotion.promotionField = move.targetField;
                EngineEventData eventData = new EngineEventData(EngineEventType.PROMOTION_DIALOG, move.piece.color);
                onEngineEvent(this, eventData);
                return;
            }

            bool isValid = _currAllowedMoves.Exists((Move m) => { return m == move; });
            if ( isValid && move.piece.color == _playingColor)
            {
                _lastMove = move;

                if (Castle.isCastle(move))
                {
                    Castle.execute(_board, move);
                }
                else
                {
                    Utils.move(_board, move, false);
                }

                endRound();
            }

        }

        private void endRound()
        {
            _board.blackChecked = Utils.isChecked(_board, ChessColor.Black);
            _board.whiteChecked = Utils.isChecked(_board, ChessColor.White);

            EngineEventData eventData = new EngineEventData(EngineEventType.BOARD_CHANGED, _board, _lastMove);
            onEngineEvent(this, eventData);

            if (_playingColor == ChessColor.White) _playingColor = ChessColor.Black;
            else _playingColor = ChessColor.White;
            eventData = new EngineEventData(EngineEventType.CHANGE_TURNS, _playingColor);
            onEngineEvent(this, eventData);
        }

        private void executePromotion(Piece.Type type)
        {
            _waitForPromotion = false;
            Promotion.promote(_board, type);
            endRound();
        }

    }
}
