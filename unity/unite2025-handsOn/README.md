## Unite 2025 HandOn Multiplay - Distributed Authority

> ### 초기 세팅
- Services > General Settings > 조직과 프로젝트 등록
- Windows > Multiplayer > Multiplayer Center에서 프로젝트 세팅
- Windows > Multiplayer > Multiplayer play mode에서 다른 플레이어 창 열어서 테스트 가능!
  - 메인 플레이어 play하면 테스트 플레이어도 같이 시작하여 동기화되며, 활성화된 창의 입력을 반영함

<br>

> ### IP & Port 연결
- Hierarchy > NetworkManager
  - NetworkBehaviour 상속, Client hosted game
  - 연결된 프리팹에 Network Object 컴포넌트가 적용되어 서로 감지할 수 있게 됨

→ 일반적인 게임은 개별 IP&Port에 접속하는 것이 아니라 로비를 만들어서 접속하는 형식인데..!

<br>

> ### 분산 네트워크 설정
- Hierarchy > 우클릭 > Multiplayer widgets > create > create sessions
- Multiplayer widgets > join and leave > session list
- Multiplayer widgets > join and leave > quick join session
- Newtork Manager의 transport component 제거 & network topology를 분산 권한으로 변경

<br>

> ### 기타 설정
- NetworkManager 내부에 할당된 Prefab을 삭제하여 바로 스폰되지 않도록 함
