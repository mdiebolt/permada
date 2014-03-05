using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Player {
  public class Combo {
    private List<string> keys;
    public string Name;

    public Combo(List<string> comboKeys) {
      keys = comboKeys;
      // Default combo name to first key plus length
      Name = string.Format("{0} x{1}", comboKeys[0], comboKeys.Count);
    }
    
    public bool Matches(List<string> potentialCombo) {
      return potentialCombo.SequenceEqual(keys);
    }
  }
}