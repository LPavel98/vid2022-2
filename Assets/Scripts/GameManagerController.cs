using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

public class GameManagerController : MonoBehaviour
{
    
    public Text scoreText;
    public Text livesText;
    private int score;
    private int lives;

    
    // Start is called before the first frame update
    void Start()
    {
        score = 0;
        lives = 3;
        PrintScoreInScreen();
        PrintLivesInScreen();
        LoadGame();
        
    }

    public void SaveGame(){
        var filePath = Application.persistentDataPath + "/save.dat";
        FileStream file;
        if (File.Exists(filePath))
        {
            file = File.OpenWrite(filePath);
        }
        else
        {
            file = File.Create(filePath);
        }
        GameData data = new GameData();
        data.Score = score;
        BinaryFormatter bf = new BinaryFormatter();
        bf.Serialize(file, data);
        file.Close();

    }
    public void LoadGame(){
        var filePath = Application.persistentDataPath + "/save.dat";
        FileStream file;
        if (File.Exists(filePath))
        {
            file = File.OpenRead(filePath);
        }
        else
        {
            Debug.LogError("No se encontó archivo");
            return;
        }
        BinaryFormatter bf = new BinaryFormatter();
        GameData data = (GameData) bf.Deserialize(file);        
        file.Close();
        score = data.Score;
        PrintScoreInScreen();

    }
    // Update is called once per frame
    public int Score(){
        return score;
    }

    public int Lives(){
        return lives;
    }

    public void GanarPuntos(int puntos){
        score += puntos;
        PrintScoreInScreen();
    }
    public void PerderVida(){
        lives -= 1;
        PrintLivesInScreen();
        if (lives == 0)
        {
            Debug.Log("Se quedo´sin vids");
            livesText.text = "GAME OVER";
            
           
        }
    }

    private void PrintScoreInScreen(){
        scoreText.text = "Puntaje: " + score;
    }

    
    private void PrintLivesInScreen(){
        livesText.text = "Visa: " + lives;
    }
}
