using UnityEngine;
using UnityEngine.UI;
using System;

public class TicTacToe : MonoBehaviour
{
    // checker = false => X turn, true => O turn
    private bool checker;
    private int plusone;
    private int moveCount;

    public Text TicBtn1 = null;
    public Text TicBtn2 = null;
    public Text TicBtn3 = null;
    public Text TicBtn4 = null;
    public Text TicBtn5 = null;
    public Text TicBtn6 = null;
    public Text TicBtn7 = null;
    public Text TicBtn8 = null;
    public Text TicBtn9 = null;

    public Text msgFeedback = null;

    public Text txtPlayerX = null;
    public Text txtPlayerO = null;

    private bool gameOver;

    // -------------------------
    // Button click handlers
    // (Hook these up in Inspector)
    // -------------------------
    public void TicBtn1Click() => PlaceMark(TicBtn1);
    public void TicBtn2Click() => PlaceMark(TicBtn2);
    public void TicBtn3Click() => PlaceMark(TicBtn3);
    public void TicBtn4Click() => PlaceMark(TicBtn4);
    public void TicBtn5Click() => PlaceMark(TicBtn5);
    public void TicBtn6Click() => PlaceMark(TicBtn6);
    public void TicBtn7Click() => PlaceMark(TicBtn7);
    public void TicBtn8Click() => PlaceMark(TicBtn8);
    public void TicBtn9Click() => PlaceMark(TicBtn9);

    private void PlaceMark(Text btnText)
    {
        if (gameOver) return;
        if (!string.IsNullOrEmpty(btnText.text)) return; // already taken

        btnText.text = checker ? "O" : "X";
        moveCount++;

        ScoreAndCheckEnd();

        // switch turns only if game didn't end
        if (!gameOver)
        {
            checker = !checker;
            msgFeedback.text = checker ? "Player O Turn" : "Player X Turn";
        }
    }

    // -------------------------
    // Game control buttons
    // -------------------------
    public void StartGame()
    {
        ResetBoardOnly();
        checker = false; // X starts
        msgFeedback.text = "Player X Turn";
        gameOver = false;
    }

    public void ResetGame()
    {
        ResetBoardOnly();
        txtPlayerX.text = "0";
        txtPlayerO.text = "0";
        checker = false;
        msgFeedback.text = "Player X Turn";
        gameOver = false;
    }

    private void ResetBoardOnly()
    {
        ClearCell(TicBtn1);
        ClearCell(TicBtn2);
        ClearCell(TicBtn3);
        ClearCell(TicBtn4);
        ClearCell(TicBtn5);
        ClearCell(TicBtn6);
        ClearCell(TicBtn7);
        ClearCell(TicBtn8);
        ClearCell(TicBtn9);

        moveCount = 0;
        gameOver = false;
    }

    private void ClearCell(Text t)
    {
        t.text = "";
        t.color = Color.black;
    }

    // -------------------------
    // Scoring / win checking
    // Vertical numbering layout:
    // Column 1: 1,2,3
    // Column 2: 4,5,6
    // Column 3: 7,8,9
    // Therefore rows are:
    // Top row:    1,4,7
    // Middle row: 2,5,8
    // Bottom row: 3,6,9
    // -------------------------
    private void ScoreAndCheckEnd()
    {
        // Check X wins
        if (CheckWinner("X"))
        {
            msgFeedback.text = "Player X Wins!";
            plusone = int.Parse(txtPlayerX.text);
            txtPlayerX.text = Convert.ToString(plusone + 1);
            gameOver = true;
            return;
        }

        // Check O wins
        if (CheckWinner("O"))
        {
            msgFeedback.text = "Player O Wins!";
            plusone = int.Parse(txtPlayerO.text);
            txtPlayerO.text = Convert.ToString(plusone + 1);
            gameOver = true;
            return;
        }

        // Draw
        if (moveCount >= 9)
        {
            msgFeedback.text = "It's a Draw!";
            gameOver = true;
        }
    }

    private bool CheckWinner(string s)
    {
        // Columns (vertical in your numbering)
        if (LineMatch(TicBtn1, TicBtn2, TicBtn3, s)) return true;
        if (LineMatch(TicBtn4, TicBtn5, TicBtn6, s)) return true;
        if (LineMatch(TicBtn7, TicBtn8, TicBtn9, s)) return true;

        // Rows (horizontal in your numbering)
        if (LineMatch(TicBtn1, TicBtn4, TicBtn7, s)) return true;
        if (LineMatch(TicBtn2, TicBtn5, TicBtn8, s)) return true;
        if (LineMatch(TicBtn3, TicBtn6, TicBtn9, s)) return true;

        // Diagonals
        if (LineMatch(TicBtn1, TicBtn5, TicBtn9, s)) return true;
        if (LineMatch(TicBtn3, TicBtn5, TicBtn7, s)) return true;

        return false;
    }

    private bool LineMatch(Text a, Text b, Text c, string s)
    {
        if (a.text == s && b.text == s && c.text == s)
        {
            a.color = Color.green;
            b.color = Color.green;
            c.color = Color.green;
            return true;
        }
        return false;
    }
}
