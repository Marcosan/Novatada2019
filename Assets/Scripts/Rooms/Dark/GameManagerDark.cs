using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GameManagerDark : MonoBehaviour {

    public Tilemap DarkMap;
    public Tilemap BlurredMap;
    public Tilemap BackgroundMap;

    public Tile DarkTile;
    public Tile BlurredTile;

    // Start is called before the first frame update
    void Start(){
        Vector3 parentPos = BackgroundMap.transform.position;
        DarkMap.origin = BlurredMap.origin = (new Vector3Int((int)parentPos.x, (int)parentPos.y, (int)parentPos.z));
        DarkMap.size = BlurredMap.size = BackgroundMap.size;
        DarkMap.transform.parent.position = BackgroundMap.transform.parent.position;

        for (int n = BackgroundMap.cellBounds.xMin; n < BackgroundMap.cellBounds.xMax; n++){
            for (int p = BackgroundMap.cellBounds.yMin; p < BackgroundMap.cellBounds.yMax; p++){
                Vector3Int localPlace = (new Vector3Int(n, p, (int)BackgroundMap.transform.position.y));
                Vector3 place = BackgroundMap.CellToWorld(localPlace);
                BlurredMap.SetTile(localPlace, BlurredTile);
                DarkMap.SetTile(localPlace, DarkTile);
            }
        }

    }

    // Update is called once per frame
    void Update(){
        
    }
}
