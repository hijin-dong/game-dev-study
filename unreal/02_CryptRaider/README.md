# CryptRaider
Developed with Unreal Engine 5

- 조작 방법
  - WASD 키로 이동 + 마우스로 시야 전환
  - 스페이스 바로 점프
- 게임 규칙
  - 미궁 가장 깊은 곳에 있는 황금 조각상 도굴하기!

<br>

> [에셋 파일 내려 받은 뒤](https://www.fab.com/listings/c13bd0dc-ac4d-4595-b284-f81386b2e6ef) Content 폴더에 넣어주어야 레벨이 나타남
<br>

## To-Do-List
- [x] 1️⃣ Design level
- [x] 2️⃣ Add lighting using lumen
- [x] 3️⃣ Spawn movable first-person player
- [x] 4️⃣ Add Grabber to grab object
- [x] 5️⃣ Add structures that move according to puzzle elements

<br>

## Notes

### 레벨 수정
- 파란 네모: Grid Snap Settings > 단위 변경하면 격자 단위로 이동 가능
- 빨간 네모: 4분할로 나누어서 작업 가능

<br>

### 조명
- 조명 종류 (Quick add > Lights)

|Point Light|Spot Light|Rect Light|Directional Light|Sky Light|
|---|---|---|---|---|
|1광원|1광원, 방향성|면광원, 방향성|햇빛 표현용 <br> 방향성, 위치 상관 X|레벨 전체를 구로 감싸서 <br> 하늘과 지평선 시뮬레이션|

<br>

- 일반적으로 Sphere + SkyLight + DirectionalLight 조합하여 사용
  - Sphere
    - Content Drawer > Settings > Show Engine Content > 생성된 Engine 폴더 진입 > Sky 검색하여 BP_Sky_Sphere 레벨에 추가
    - Details > Directional Light Actor에 DirectionalLight 컴포넌트 추가 > 이후로는 각도 조절하고 Sphere Details 에서 Refresh Material 하면 태양 위치 변경 및 이에 따른 시간대 반영됨
  - SkyLight
    - Details > Sky Light > Recapture Scene > Recapture 버튼 클릭하여 조명 업데이트

※ SkyLight와 DirectionalLight 모두 Detail > Mobility 에서 Movable로 설정하면 조명 효과를 실시간으로 계산

<br>

**조명 추가에 따른 메터리얼 문제 해결**
- Content Drawer에서 확인해보면 파일명에 Inst가 붙어있음 -> 실제 메터리얼이 아니라 텍스처와 설정값으로 이루어진 블루프린트
- 우클릭하여 Find Parent > 클릭하여 편집기 열기 > 루멘이 지원하지 않는 Pixel Depth Offset 연결 해지 > apply

<br>

### 레벨에 Collision 설정
- Content Drawer > 바닥/벽 메시 선택 > 편집기 상단 툴바 > Collision > Add Box Collision
- Details > Primitives 에서 추가한 Box Collision 속성 변경 가능 > z 값 부여하여 두께 추가 + center z 값을 두께만큼 음수로 부여해서 캐릭터가 떠다니지 않도록 설정

<br>

### 플레이어 커스텀
- Content > FirstPerson > Blueprints > Create child blueprint > Components에서 Mesh 1P 선택 후, Details에서 메시 제거
- 게임 모드 생성
  - 레벨 블루프린트 생성 버튼 > GameMode > Create > 현재 프로젝트 게임 모드 선택
  - 편집기 Details > Default Pawn Class에서 만들었던 플레이어 블루프린트 적용
  - Project Settings에서 게임모드 적용

<br>

### 액터 컴포넌트 생성
- Details > Add > New C++ component 
- 코드 작성 후, Add > 작성한 컴포넌트 추가

<br>

### 액터 주소 접근 및 프로퍼티 가져오기
```
# 해당 컴포넌트를 소유한 액터 주소를 변수에 저장
AActor* owner = GetOwner();

# 프로퍼티 가져오기
FString name = owner->GetActorNameOrLabel();
FVector ownerLocation = owner->GetActorLocation();
```
