# ToonTanks
 
## Notes

> ### 전방 선언
- 컴포넌트 사용 시 include가 필요한 경우
  - 헤더 파일에는 class를 붙여주고
  - cpp 파일에서 실제로 사용할 때 include를 해주기
→ 컴파일 시간 단축 가능

<br>

> ### 객체 이동에 따른 화면 전환 (카메라 추가)
- Blueprint 이용
  - Root Component 선택 > Spring Arm 추가 > Sprint Arm 선택 후 Camera 추가
- C++ 이용
  - USprintArmComponent & UCameraComponent
※ 카메라 직접 조작 X, 스프링 암 이용

<br>

> ### 플레이어블 폰 설정
- Details > Auto Possess Player > Player0으로 설정
※ 플레이어 숫자는 네트워크 기반 멀티플레이어가 아닌 동일 기기 내 플레이어 의미

<br>

> ### Input 처리
- Project Setting > Input > Binding에 받고자 하는 Input 설정
  - Action Mappings
  - Axis Mappings
- Player 클래스 (Tank)에 콜백함수 추가
```
// Move & Turn 함수를 전방 이동 매핑에 바인드
void ATank::SetupPlayerInputComponent(UInputComponent* PlayerInputComponent)
{
    Super::SetupPlayerInputComponent(PlayerInputComponent);
    PlayerInputComponent->BindAxis(TEXT("MoveForward"), this, &ATank::Move);
    PlayerInputComponent->BindAxis(TEXT("Turn"), this, &ATank::Turn);
}
```

<br>

> ### Move 함수 구현
```
void ATank::Move(float Value)
{
    FVector DeltaLocation = FVector::ZeroVector;
    DeltaLocation.X = Value * Speed * UGameplayStatics::GetWorldDeltaSeconds(this);
    
    AddActorLocalOffset(DeltaLocation, true);
    // 1) DeltaTime은 Tick 함수에서는 기본적으로 얻을 수 있으나, Tick 밖에서는 받아와야 함
    // 2) 충돌 감지. Root Component 기준으로만 감지하며 Collision Preset 설정
}
```

<br>

> ### 버그: 🔧 Simulate Physics가 이동 로직에 미치는 영향
|항목|영향|
|----|----|
|Simulate Physics = true|Actor는 물리엔진이 제어함 → AddActorLocalOffset, SetActorLocation 같은 직접 위치 변경은 예측 불가|
|Simulate Physics = false|Actor는 코드로 이동 제어 가능 → AddActorLocalOffset, AddActorWorldOffset 등 정상 작동|

※ 추후에 Physics Simulation과 직접 이동을 같이 쓰고 싶다면 UPrimitiveComponent::AddForce()나 AddImpulse()를 사용해야 함!

<br>

> ### Cast
- C++의 dynamic_cast<T*> → Unreal의 Cast<T>
```
    PlayerControllerRef = Cast<APlayerController>(GetController());
```

<br>

> ### Line Trace
```
        FHitResult HitResult;
        PlayerControllerRef->GetHitResultUnderCursor(
            ECollisionChannel::ECC_Visibility, // Visibility Channel에서
            false, // 디테일한 판정 여부 (성능에 영향)
            HitResult // 구조체에 hit 결과 return
        );
```

<br>

> ### 버그: 벽쪽에 마우스를 가져다대면 터렛이 휙 도는 현상
- 원인: Hit 대상이 없어서 레벨 중앙을 가리키게 됨
- 해결: 눈에 보이지 않는 Hit 대상을 만들어주기
  - Quick Add > Place Actors > Volumes > Blocking Volume 추가
  - Collision Preset을 보면 Visibility Channel이 block 설정이 되어있지 않음 & Line Trace 설정을 Visibility Channel로 해두었으므로 커스텀 프리셋 설정을 통해 block

<br>

> ### 버그: 터렛 근처에서 마우스를 움직일 경우 덜컹이는 현상
- 원인: 값이 너무 빠르게 변동
- 해결: 보간
  - FMath::RInterpTo() 함수 사용 (Rotation Interpolation)
  - 해당 함수는 공통으로 사용할 수 있도록 BasePawn에 추가

<br>

> ### 탱크 위치 가져오기 & 조준하기
- UGameplayStatics::GetPlayerPawn() 함수 이용
- EditDefaultsOnly를 이용해서 추적 반경을 추후에도 모두 동일하도록 설정

※ 내용 적용 후, 타워 블루프린트에서 클래스 설정을 Tower로 변경해주어야 반영됨

<br>

> ### Fire 함수 구현
  - Action Mappings 이용
  - Pressed / Released의 두 가지 옵션 제공
※ Axis Mapping: 매 프레임 실행 / Action Mapping: 버튼을 누르는 순간 실행

<br>

> ### Timer
- FTimerManager: 월드읱 타이머 관리
  - SetTimer()
    - FTimerHandle을 인자로 받아 각 타이머별로 설정
    - Timer Rate로 타이머 실행까지의 delay 추가
    - 콜백함수

<br>

> ### Projectile
- 블루프린트를 통해 추가한 정적 메쉬를 포함한 Projectile을 C++ 코드로 생성하고 싶은 상황!
  - TSubclassOf<>: type parameter를 받고 UClass를 저장할 수 있도록 해줌
  - UClass: C++ ~ 블루프린트 간의 정보 교환 가능
```
// Projectile C++ class를 기반으로 한 발사체 클래스를 설정할 수 있게 됨
UPROPERTY(EditDefaultsOnly, Category = "Combat")
TSubclassOf<class AProjectile> ProjectileClass;
```
