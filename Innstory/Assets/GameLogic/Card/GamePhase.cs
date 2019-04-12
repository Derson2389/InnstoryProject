/// <summary>
/// 游戏阶段
/// </summary>

public enum GamePhase
{

    #region  战斗阶段
    ChooseMissionCard,  //挑选任务卡
    MissionCardCaculte, //任务卡结算抽卡
    
    PutItemCard,        //打出装备卡
    ChoosSkillCard,     //选择技能卡
    DestoryCard,        //烧卡
    Throw,              //筛子投掷
    CounterCard,        //轨迹卡
    BattlePlay,         //战斗动画
    Settlement,         //任务结算  
    #endregion
}

