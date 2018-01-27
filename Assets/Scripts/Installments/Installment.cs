using UnityEngine;
using System.Collections.Generic;

public class Installment : MonoBehaviour {
  public static List<Installment> ListOfInstallments = new List<Installment>();

  public bool EnemyShouldIgnore = false;

  protected void Start()
  {
    ListOfInstallments.Add(this);
  }

  protected void OnDestroy()
  {
    ListOfInstallments.Remove(this);
  }
}