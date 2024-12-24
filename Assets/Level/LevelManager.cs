using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public Transform container;

    public List<GameObject> levels;

    public float timeBetweenPieces = .1f;

    public List<LevelPieceBasedSetup> levelPieceBasedSetups;

    [SerializeField]private int _index;
    private GameObject _currentLevel;

    [SerializeField]private List<LevelPieceBase> _spawnedPieces = new List<LevelPieceBase>();
    private LevelPieceBasedSetup _currSetup;

    private void Awake(){
        //SpawnNextLevel();
        CreateLevelPieces();
    }

    private void SpawnNextLevel(){
        if(_currentLevel != null){
            Destroy(_currentLevel);
            _index++;
            if(_index >= levels.Count){
                ResetLevelIndex();
            }
        }
        _currentLevel = Instantiate(levels[_index], container);
        _currentLevel.transform.localPosition = Vector3.one;
    }

    private void ResetLevelIndex(){
        _index = 0;
    }

    private void CreateLevelPieces(){
        CleanSpawnedPieces();

        if(_currSetup != null){
            _index++;

            if(_index >= levelPieceBasedSetups.Count){
                ResetLevelIndex();
            }
        }
        _currSetup = levelPieceBasedSetups[_index];

        for (int i = 0; i < _currSetup.piecesStartNumber; i++){
            CreateLevelPiece(_currSetup.levelPiecesStart);
        }

        for (int i = 0; i < _currSetup.piecesNumber; i++){
            CreateLevelPiece(_currSetup.levelPieces);
        }

        for (int i = 0; i < _currSetup.piecesEndNumber; i++){
            CreateLevelPiece(_currSetup.levelPiecesEnd);
        }

        ColorManager.Instance.ChangeColorByType(_currSetup.artType);
        
    }

    private void CreateLevelPiece(List<LevelPieceBase> list){
        var piece = list[Random.Range(0, list.Count)];
        var spawnedPiece = Instantiate(piece, container);

        if(_spawnedPieces.Count > 0){
            var lastPiece = _spawnedPieces[_spawnedPieces.Count - 1];

            spawnedPiece.transform.position = lastPiece.endPiece.position;
        }
        else{
            spawnedPiece.transform.localPosition = Vector3.zero;
        }

        foreach(var p in spawnedPiece.GetComponentsInChildren<ArtPiece>()){
            p.ChangePiece(ArtManager.Instance.GetSetupByType(_currSetup.artType).gameObject);
        }

        _spawnedPieces.Add(spawnedPiece);
    }

    private void CleanSpawnedPieces(){
        for(int i = _spawnedPieces.Count - 1; i >= 0; i--){
            Destroy(_spawnedPieces[i].gameObject);
        }
        _spawnedPieces.Clear();
    }

    IEnumerator CreateLevelPiecesCoroutine(){
        _spawnedPieces = new List<LevelPieceBase>();

        for(int i = 0; i < _currSetup.piecesNumber; i++){
            CreateLevelPiece(_currSetup.levelPieces);
            yield return new WaitForSeconds(timeBetweenPieces);
        }
    }

    private void Update(){
        if(Input.GetKeyDown(KeyCode.D)){
            CreateLevelPieces();
        }
    }
}
