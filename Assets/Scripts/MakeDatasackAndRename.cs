using System.Collections;
using AssemblyTest;
using UnityEditor;
using UnityEngine;

namespace ReproBugRename
{
    public class MakeDatasackAndRename : MonoBehaviour
    {
        private class PlayerDataTest : ScriptableObject
        {
            public int HpMax;
        }

        [SerializeField] private bool cleanupFile;
        private PlayerDataTest _p;
        private PlayerDataTestAssemblyDef _pAssembly;
        IEnumerator Start()
        {
            _p = ScriptableObject.CreateInstance<PlayerDataTest>();
            _pAssembly = ScriptableObject.CreateInstance<PlayerDataTestAssemblyDef>();
            AssetDatabase.CreateAsset( _p, "Assets/New Datasack.asset");
            AssetDatabase.CreateAsset( _pAssembly, "Assets/New Datasack Assembly.asset");
            
            Debug.Log( "Go look at the Datasack, see the initial value.");
 
            AssetDatabase.Refresh();
            
            //Wait for modification
            yield return new WaitUntil( () => { return Input.anyKeyDown; } );
            yield return null;
            
            _p.HpMax = 10;
            _pAssembly.HpMax = 10;
            
            Debug.Log( "Go look at the Datasack, see the modify.");
 
            Debug.Log( "Press a key to rename.");
 
            yield return new WaitUntil( () => { return Input.anyKeyDown; } );
            yield return null;
 
            Debug.Log( "Renaming now.");
 
            RenameDatasacks();
 
            Debug.Log( "It has been renamed, go look now. I'll wait...");
        }
 
        private void RenameDatasacks()
        {
            AssetDatabase.RenameAsset(AssetDatabase.GetAssetPath(_p), "Datasack{" + 10 + "}");
            AssetDatabase.RenameAsset(AssetDatabase.GetAssetPath(_pAssembly), "Datasack Assembly{" + 10 + "}");
        }
        
        private void OnDestroy()
        {
            if (!cleanupFile) return;
            AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(_p));
            AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(_pAssembly));
        }
    }
}