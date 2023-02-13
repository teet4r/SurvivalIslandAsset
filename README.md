# Project: Survival Island

</br>

## 1. 개요
- 오랫동안 몬스터들로부터 생존하는 간단한 게임

</br>

## 2. 제작기간 & 참여인원
- 약 20일
- 개인 프로젝트

</br>

## 3. 사용언어 & 도구
- C#
- Visual Studio
- Unity 3D(에디터 버전: 2019.4.40f1)

</br>

## 4. 구동 플랫폼
- Windows(PC)

</br>

## 5. 주요 구현 이슈
<details>
<summary>오브젝트 풀링</summary>
<div markdown="1">

- 소환되는 몬스터 재사용

</div>
</details>

<details>
<summary>커스텀 업데이트 매니저 시험 적용</summary>
<div markdown="1">

- 게임 오브젝트에서 실행되는 Update() 문을 커스텀
- Update(), FixedUpdate(), LateUpdate()만 실행하는 매니저 오브젝트를 생성하여 관리

</div>
</details>

<details>
<summary>총알 생성 로직 변경</summary>
<div markdown="1">

- 기존에는 총알 오브젝트를 생성하여 날아간 후, 목표물을 타격하면 제거되는 방식
- 이를 Raycast로 처리하여 즉발 형식으로 변경, 총구에서 목표물 타격 지점 두 곳을 이어주는 총알 궤적만 생성함

</div>
</details>

<details>
<summary>애니메이션 적용</summary>
<div markdown="1">

- 플레이어는 뛰는 애니메이션, 재장전 애니메이션 등을 추가하여 조금 더 자연스럽게 표현
- 몬스터들은 손이나 무기에 콜라이더를 부착하여 애니메이션에 맞게 공격

</div>
</details>

<details>
<summary>UI</summary>
<div markdown="1">

- 왼쪽 하단에 미니맵을 생성하여 현재 나의 위치를 표시
- 플레이어 화면에 조준점을 생성하여 타격 지점이 어디인지 표시

</div>
</details>

</br>

## 6. 스크린샷
<details>
<summary>메인화면</summary>
<div markdown="1">

![SurvivalIsland Main1](https://user-images.githubusercontent.com/76508241/218364596-dfc6fbc6-5a7e-4ac0-8b8c-70abaa2393bf.png)
- Play: 게임 시작
- Quit: 어플리케이션 종료

</div>
</details>

<details>
<summary>인게임</summary>
<div markdown="1">

![SurvivalIsland Ingame1](https://user-images.githubusercontent.com/76508241/218364600-d8b7fd0a-5f9d-4391-bdca-3a89468d2ec2.png)
![SurvivalIsland Ingame2](https://user-images.githubusercontent.com/76508241/218364606-d94ced69-805f-42c8-bcdb-5998305a6414.png)
![SurvivalIsland Ingame3](https://user-images.githubusercontent.com/76508241/218364610-a4fd7303-ed52-4ead-9887-a073ec38d5b7.png)
- 조준섬을 통해 어느 곳을 조준하고 있는지 확인 가능
- 몬스터의 체력바를 화면 상에 표시하여 남은 체력 확인 가능
- 왼쪽 하단에 미니맵을 표시하여 플레이어 위치 확인 가능
- 왼쪽 상단에 플레이어 체력 확인 가능

</div>
</details>

<details>
<summary>게임오버</summary>
<div markdown="1">

![SurvivalIsland End1](https://user-images.githubusercontent.com/76508241/218364613-1a4ae93e-e5bb-4be2-8205-7972fcc0cf37.png)
- 몬스터 킬 수를 표시
- Play: 인게임에서 다시 시작
- Quit: 어플리케이션 종료

</div>
</details>

</br>

## 7. 링크
- [플레이 영상 및 다운로드](https://drive.google.com/drive/folders/1-nM-7JMnqMflJsSRfKCR90vZg1sbSQIP)
