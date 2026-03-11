# ObstacleAssault
Developed with Unreal Engine 5

- 조작 방법
  - WASD 키로 이동 + 마우스로 시야 전환
  - 마우스 스크롤 클릭 시 특수 액션!
  - 스페이스 바로 점프
- 게임 규칙
  - 장애물을 피해 엔딩 지점까지 도달
<br>

## To-Do-List
- [x] 1️⃣ Create a project with assets
- [x] 2️⃣ Make and configure moving platform
- [x] 3️⃣ Make it round trip
- [x] 4️⃣ Make and configure rotating platform
- [x] 5️⃣ Design level
<br>

## Notes
- 기본 레벨 설정
  - Settings > Project Settings > Maps&Modes > Default Maps를 현재 레벨로 설정 
- 플레이어 설정
  - Blueprint > Details > Auto Possess Player를 Player 0으로 설정
<br>

**클래스 생성**
- 상단 툴바 > Tools > New C++ Class
- VS에서 빌드하면 Content Drawer에 추가됨
※ 컴파일 전에 에디터 종료 필수

<br>

**변수 추가**
- UPROPERTY 키워드 표시하여 엔진이 인지할 수 있도록 표시
  - EditAnywhere/VisibleAnywhere, Category=""와 같은 인수 넣을 수 있음
- 이후부터는 Live Coding 기능 이용하여 에디터 종료하지 않고 컴파일 가능
※ 추가한 프로퍼티가 사라지거나 변경값이 초기화되는 경우, 에디터 종료 후 VS에서 빌드

<br>

**게임모드**
- 레벨 내에서 게임 규칙을 관리하는 액터
  - 누가 어디에 스폰되어야 하는지
  - 멀티플레이어 게임에서 해당 게임에 허용되는 플레이어 수가 몇 명인지
  - 바람 조건
  - 깃발 뺏기, 배틀로얄
  - ...
- 툴바 > 블루프린트 드롭다운
  - Project Settings: 프로젝트 내의 모든 레벨에 대한 기본 게임 모드
  - World Override: 특정 레벨에 대한 게임 모드

<br>

**게임모드로 플레이어 설정**
- 블루프린트 에디터 > Details > Classes 섹션 > Default Pawn Class에서 설정
- World Override에 있던 기존 플레이어 삭제 및 Project Setting에서 새로 만든 모드 적용
- 레벨에 생성되어 있는 현재 캐릭터 삭제 > Quick Add > Place Actor > Player Start를 Scene으로 끌어오기
→ 게임 시작 & 마우스 우클릭 후 "여기서 시작" 옵션 사용 가능

<br>

**로그 출력하기**
```
UE_LOG(LogTemp, Display, TEXT("huhu"));
# 순서대로 카테고리, 표시 수준, 내용
# C형식의 % 포맷으로 데이터 출력 가능

# Vector와 같은 기본 C 데이터 형식이 아닌 경우, 아래와 같이 FString 결합하여 사용
FString huhu = "huhu";
UE_LOG(LogTemp, Display, TEXT("%s"), *huhu);
```
