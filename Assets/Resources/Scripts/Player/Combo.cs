using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Player {
  public delegate void callback(Combo combo);

  public class Combo {
    private List<string> keys;
    public string Name;
   
    public void Execute(callback cb) {
      cb(this);
    }

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