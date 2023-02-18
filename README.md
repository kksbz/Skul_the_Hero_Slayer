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
2023-02-17 / v0.1.1 MonsterMove 상태class 몬스터가 이동방향을 바라보는 방향전환 구현
2023-02-18 / v0.1.2 MonsterMove의 offset값에 따라 MonsterAni변경, GroundCheck하는 Raycast 구현
2023-02-18 / v0.1.2 Monster의 플레이어 탐색상태class 구현