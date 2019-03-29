﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FuncCodeItem : CodeItemBase{

    public override string Parse()
    {
        if (this.codeId == 10) Commands.Instance.Move(0, int.Parse(this.paras));
        if (this.codeId == 11) Commands.Instance.Move(1, int.Parse(this.paras));
        if (this.codeId == 12) Commands.Instance.Move(2, int.Parse(this.paras));
        if (this.codeId == 13) Commands.Instance.Move(3, int.Parse(this.paras));

        return this.paras;
    }
}
