using UnityEngine;

namespace Core.Symbol
{
    public interface ISymbol
    {
        void ActivateSymbol();
        void DeactivateSymbol();
        void OnProcessFinish();
        SymbolData GetSymbolData();
    }
}