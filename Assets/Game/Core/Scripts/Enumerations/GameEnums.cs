
namespace Utilities
{
    public static class GameEnums
    {
        public static PositionState Position;
        public enum PositionState
        {
            ROOF,
            RWALL,
            LWALL,
            GROUND,
            RCORNER,
            LCORNER,
            RCORNER_UP,
            LCORNER_UP,
            OUTOFBOUNDS,
        }

        public static SymbolID SymbolId;
        public enum  SymbolID
        {
            A,
            B,
            C,
            D,
            E,
            F,
            Ring,
            Square
        }

        public enum PlayerState
        {
            OnTile,
            OnAir,
        }
        
    }
}
