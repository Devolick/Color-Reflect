using UnityEngine;
using System.Collections;

public class MagniteSupplyArea : GameSubject {
    [SerializeField]
    public MagniteSupply MagniteSupplyLeft { get; set; }
    public MagniteSupply MagniteSupplyRight { get; set; }

    protected override void _Awake()
    {
        base._Awake();
        FindMagniteSupplys();
    }
    public override void AskYou(Base sender)
    {
        base.AskYou(sender);
        if (sender.name == "TableArea")
        {
            (sender as TableArea).Reconnect(this);
        }
    }
    void FindMagniteSupplys() {
        MagniteSupplyLeft = FindChild("SupplyMagniteLeft").GetComponent<MagniteSupply>();
        MagniteSupplyRight = FindChild("SupplyMagniteRight").GetComponent<MagniteSupply>();
    }
    public void SendBall(Base o) {
        int rd = UnityEngine.Random.Range(0, 101);
        if (rd >= 50)
        {
            MagniteSupplyLeft.SetBall();
        }
        else if (rd < 50)
        {
            MagniteSupplyRight.SetBall();
        }
    }
    public void StopSupplys() {
        MagniteSupplyLeft.Stop();
        MagniteSupplyRight.Stop();
    }













}
