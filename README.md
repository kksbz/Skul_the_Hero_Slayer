# Skul_the_Hero_Slayer
2023-02-13 Project Setup<br/>
2023-02-13 / v0.0.1 TitleScene setup<br/>
2023-02-13 / v0.0.2 Player Move, DoubleJump, dash<br/>
2023-02-13 / v0.0.3 FallAni setup, camara setup<br/>
2023-02-15 / v0.0.5 Loading, DungeonLobby setup<br/>
2023-02-16 / v0.0.6 Work on Asset preparation<br/>
2023-02-16 / v0.0.7 Work on MonsterAsset preparation<br/>
2023-02-16 / v0.0.8 Work on MonsterFrefab<br/>
2023-02-17 / v0.0.9 work on MonsterFSM_AI<br/>
            Summary :   v0.0.9 몬스터AI 구현시도<br/>
            detail  :   몬스터AI를 상태패턴으로 구현하고자 시도함<br/>
                        몬스터class(최상위) 상속 -> 타입별 몬스터 class 작성<br/>
                        몬스터가 행할 행동의 상태를 상태패턴으로 구현하고자 스크립트를 나눠서 작성함<br/>
                        각상태가 수행해야할 로직을 interface로 강제시키고 각 상태별로 class를 나눔<br/>
                        몬스터class는 데이터만 갖고있는 껍데기로 설정<br/>
                        몬스터의 상태를 컨트롤할 몬스터컨트롤러 작성<br/>
                        몬스터컨트롤러에서 입력받은 상태를 처리할 StateMachine class작성<br/>
                        각 상태로직을 수행할 interface를 상속받은 상태class에서<br/>
                        몬스터의 데이터를 참조하는 방식을 찾지못해 진행이 막힘<br/>
2023-02-17 / v0.1.0 work on MonsterAI by StatePattern<br/>
            Summary :   v0.0.9 이슈 해결<br/>
            detail  :   각 상태class에서 몬스터의 데이터에 접근할 수 있는 방식을 찾음<br/>
                        몬스터컨트롤러에서 몬스터클래스를 변수로 가지고있고<br/>
                        몬스터를 상속받은 각타입별클래스 안에서 몬스터컨트롤러의 몬스터변수에<br/>
                        자신을 직접 Monster로 캐스팅해서 대입하는 방식으로 해결함<br/>
                        자식클래스가 부모클래스타입으로 캐스팅이 가능한것을 알게됨<br/>
                        (Monster)(this as Monster)로 예외처리까지 완료<br/>
2023-02-17 / v0.1.1 MonsterMove 상태class 몬스터가 이동방향을 바라보는 방향전환 구현<br/>
2023-02-18 / v0.1.2 MonsterMove의 offset값에 따라 MonsterAni변경, GroundCheck하는 Raycast 구현<br/>
2023-02-18 / v0.1.3 Monster의 플레이어 탐색상태class 구현<br/>
2023-02-19 / v0.1.4 Monster의 공격상태class 및 Monster공격 구현<br/>
            Summary :   Monster의 이동, 탐색, 공격 상태에서의 방향전환 체크문제<br/>
            detail  :   Monster의 이동, 탐색, 공격 상태가 서로 다른 class로 구현되어있어<br/>
                        상태전환시 Monster의 방향전환이 꼬이는 문제 발생<br/>
                        각 상태에서 방향전환체크하는 변수를 통일하는 것으로 문제 해결<br/>
2023-02-19 / v0.1.5 work on NomalMonster AttackState<br/>
2023-02-20 / v0.1.6 work on BigWooden AttackState<br/>
            Summary :  BigWooden 공격A,B 구현 공격B issue 발생<br/>
            detail  :  공격B 구현중 Bullet 오브젝트가 타겟과 충돌하거나 일정거리 이상 벗어나<br>
                        비활성화되어야 하는 상태의 애니메이션이 실행되지 않음<br/>
2023-02-20 / v0.1.7 Monster Setup<br/>
            Summary :   v0.1.6 이슈 해결<br/>
            detail  :   BIgWooden의 공격딜레이 코루틴함수를 공격A와B가 공유해서<br/>
                        공격타입이 전환 될 때 코루틴처리가 꼬여서 발생한 문제였음 (A,B의 애니메이션 실행이 겹침)<br/>
                        공격A,B의 딜레이 코루틴을 따로 분리해서 해결<br/>
2023-02-20 / v0.1.8 PlayerController 상태패턴으로 교체작업 시작<br/>
                    Move,Jump,Idle상태 구현완료<br/>
2023-02-20 / v0.1.9 PlayerController Dash구현<br/>
            Summary :   Dash와 점프 이동 상태 전환 조건 설정해야됨<br/>
2023-02-20 / v0.2.0 work on Asset<br/>
2023-02-21 / v0.2.1 work on Player Attack and SkillA<br/>
2023-02-21 / v0.2.2 Player Skul SkillA,B setup<br/>
2023-02-22 / v0.2.3 Player Swap Skul prototype<br/>
2023-02-22 / v0.2.4 EntSkul Ani,monsterPool setup<br/>
2023-02-23 / v0.2.5 Platform Effector2D를 사용하여 통과가능한 벽 구현, Player 또다른 캐릭터 GetEntSkul item 구현<br/>
2023-02-23 / v0.2.6 BossMonster AttackA,B prototype setup<br/>
2023-02-24 / v0.2.7 BossMonster AttackC prototype setup<br/>
2023-02-24 / v0.2.8 Work on BossMonster 2Phase<br/>
2023-02-24 / v0.2.9 BossMonster 2Phase Setup<br/>
2023-02-25 / v0.3.0 BossMonster Groggy and CropAttack Logic change<br/>
2023-02-25 / v0.3.1 Work on BossMonster Dead, Groggy, FistSlam state<br/>
2023-02-26 / v0.3.2 Player SaveData setup<br/>
2023-02-26 / v0.3.3 Player EntSkul Skill A and B, BossMonster setup<br/>
2023-02-27 / v0.3.4 Work on Ui<br/>