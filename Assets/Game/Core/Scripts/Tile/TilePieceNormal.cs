using System.Collections;
using System.Collections.Generic;
using Utilities;
using Core.Tile;
using Core.Tile.Data;
using UnityEngine;

namespace Core.Tile
{

    public class TilePieceNormal : ATile
    {
        private new void OnEnable()
        {
            base.OnEnable();
            SetTileType(TileType.Normal);
        }
    }

}
