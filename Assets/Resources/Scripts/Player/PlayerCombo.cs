using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class PlayerCombo {
  private List<string> keys;
  public string Name;
 
  public PlayerCombo(List<string> comboKeys) {
    keys = comboKeys;
    // Default combo name to first key plus length
    Name = string.Format("{0} x{1}", comboKeys[0], comboKeys.Count);
  }
  
  public bool Matches(List<string> potentialCombo) {
    return potentialCombo.SequenceEqual(keys);
  }
}
