# 구현에 사용할 패키지 임포트하기
import numpy as np  # 배열을 관리할 수 있도록 numpy 추가
import matplotlib.pyplot as plt  # 미로를 직접 그림으로 나타내기 위한 라이브러리 추가
import matplotlib.animation as animation  # 쥐의 이동을 시각화 해주는 라이브러리 추가
import matplotlib.image as mpimg  # 쥐와 치즈, 벽 이미지를 불러올 라이브러리 추가

fig = plt.figure(figsize=(7, 7))  # 전체 그림의 크기 및 그림을 나타내는 변수 선언

# 나타낼 쥐, 치즈, 벽의 이미지 미리 불러와서 저장하기
mouse_img = mpimg.imread('/Users/minjoo/Desktop/지능형게임/mouse.png')  # 쥐 이미지 불러오기
cheese_img = mpimg.imread('/Users/minjoo/Desktop/지능형게임/cheese.png')  # 치즈 이미지 불러오기
wall_img = mpimg.imread('/Users/minjoo/Desktop/지능형게임/wall.jpg')  # 벽 이미지 불러오기

# 전체 그리드(격자) 그리기
for i in range(6):  # range를 통해 i로 0 ~ 5까지 좌표 설정(5*5 배열이기 때문에)
    plt.plot([i, i], [0, 5], color='black', linewidth=1)  # x축을 따라서 이동하며 세로선 그리기
    plt.plot([0, 5], [i, i], color='black', linewidth=1)  # y축을 따라서 이동하며 세로선 그리기

ax = plt.gca()  # 현재 그림의 축(axis) 가져옴
ax.set_xlim(0, 5)  # 그림 그릴 범위 x를 0부터 5까지 설정
ax.set_ylim(0, 5)  # 그림 그릴 범위 y를 0부터 5까지 설정
plt.tick_params(axis='both', which='both', bottom=False, top=False, labelbottom=False, right=False, left=False,
                labelleft=False)  # 눈금과 레이블 제거 설정

y_1 = 0  # 초기 위치
y_2 = 5  # 끝 위치
while y_1 < y_2:  # 초기 위치는 실제 그리는 위치로 계속 변경할 것이기 때문에, 조건을 실제 위치 < 그릴 끝의 위치로 지정
    ax.imshow(wall_img, extent=(0, 0.2, y_1, y_1 + 0.2))  # 그리는 축에, imshow함수를 써서 wall_img를 그림
    y_1 += 0.2  # 한 칸 위에 벽 이미지를 그릴 수 있도록, 현재 좌표를 옮겨줌
"""
위와 같은 타일링 방법으로 나머지 벽도 그려줌
"""
# 오른쪽 벽
y_1 = 0
y_2 = 5
while y_1 < y_2:
    ax.imshow(wall_img, extent=(4.8, 5, y_1, y_1 + 0.2))
    y_1 += 0.2
# 위쪽 벽
x_1 = 1
x_2 = 5
while x_1 < x_2:
    ax.imshow(wall_img, extent=(x_1, x_1 + 0.2, 4.8, 5))
    x_1 += 0.2
# 아래쪽 벽
x_1 = 0
x_2 = 4
while x_1 < x_2:
    ax.imshow(wall_img, extent=(x_1, x_1 + 0.2, 0, 0.2))
    x_1 += 0.2

# 내벽
"""
외벽과 같은 방법으로, 강의 교안과 똑같이 그리기
가로 벽 만들기
"""
x_1 = 0.2
x_2 = 1
while x_1 < x_2:
    ax.imshow(wall_img, extent=(x_1, x_1 + 0.2, 0.9, 1.1))
    x_1 += 0.2
x_1 = 1
x_2 = 2
while x_1 < x_2:
    ax.imshow(wall_img, extent=(x_1, x_1 + 0.2, 3.9, 4.1))
    x_1 += 0.2
x_1 = 1
x_2 = 3
while x_1 < x_2:
    ax.imshow(wall_img, extent=(x_1, x_1 + 0.2, 1.9, 2.1))
    x_1 += 0.2
x_1 = 2
x_2 = 3
while x_1 < x_2:
    ax.imshow(wall_img, extent=(x_1, x_1 + 0.2, 2.9, 3.1))
    x_1 += 0.2
x_1 = 2
x_2 = 3
while x_1 < x_2:
    ax.imshow(wall_img, extent=(x_1, x_1 + 0.2, 0.9, 1.1))
    x_1 += 0.2
x_1 = 4
x_2 = 4.8
while x_1 < x_2:
    ax.imshow(wall_img, extent=(x_1, x_1 + 0.2, 1.9, 2.1))
    x_1 += 0.2

# 세로 벽 만들기
y_1 = 2
y_2 = 4
while y_1 < y_2:
    ax.imshow(wall_img, extent=(0.9, 1.1, y_1, y_1 + 0.2))
    y_1 += 0.2
y_1 = 2
y_2 = 3
while y_1 < y_2:
    ax.imshow(wall_img, extent=(3.9, 4.1, y_1, y_1 + 0.2))
    y_1 += 0.2
y_1 = 1
y_2 = 2
while y_1 < y_2:
    ax.imshow(wall_img, extent=(1.9, 2.1, y_1, y_1 + 0.2))
    y_1 += 0.2
y_1 = 3
y_2 = 4
while y_1 < y_2:
    ax.imshow(wall_img, extent=(2.9, 3.1, y_1, y_1 + 0.2))
    y_1 += 0.2
y_1 = 0
y_2 = 1
while y_1 < y_2:
    ax.imshow(wall_img, extent=(2.9, 3.1, y_1, y_1 + 0.2))
    y_1 += 0.2
y_1 = 2
y_2 = 3
while y_1 < y_2:
    ax.imshow(wall_img, extent=(4.9, 5.1, y_1, y_1 + 0.2))
    y_1 += 0.2

# 정책 파라미터 theta를 정책 pi로 변환하여 반환하는 함수를 정의합니다
def simple_convert_into_pi_from_theta(theta):
    '''단순히 값의 비율을 계산'''
    [m, n] = theta.shape
    pi = np.zeros((m, n))
    for i in range(0, m):
        pi[i, :] = theta[i, :] / np.nansum(theta[i, :])
    pi = np.nan_to_num(pi)
    return pi

# theta_0 변수를 초기화합니다 (임의의 값 사용)
theta_0 = np.random.rand(25, 4)

# 초기 정책 파라미터를 변환합니다
pi_0 = simple_convert_into_pi_from_theta(theta_0)

# 다음 상태를 계산하는 함수를 정의합니다
# 다음 상태를 계산하는 함수를 수정합니다
def get_next_s(pi, s):
    direction = ["up", "right", "down", "left"]
    next_direction = np.random.choice(direction, p=pi[s, :])
    if next_direction == "up" and s >= 5:  # 유효한 범위 내에 있는지 확인합니다
        s_next = s - 5
    elif next_direction == "right" and s % 5 != 4:
        s_next = s + 1
    elif next_direction == "down" and s < 20:
        s_next = s + 5
    elif next_direction == "left" and s % 5 != 0:
        s_next = s - 1
    else:
        s_next = s
    return s_next


# 목표 지점까지의 경로를 계산하는 함수를 정의합니다
def goal_maze(pi):
    s = 0
    state_history = [0]
    while True:
        next_s = get_next_s(pi, s)
        state_history.append(next_s)
        if next_s == 24:
            break
        else:
            s = next_s
    return state_history

# 목표 지점까지의 경로를 계산합니다
state_history = goal_maze(pi_0)

# 초기화 함수를 정의합니다
def init():
    mouse_img.set_extent((0.2, 0.8, 4.2, 4.8))
    return (mouse_img,)

# 애니메이션 생성 함수를 정의합니다
def animate(i):
    state = state_history[i]
    x = (state % 5) + 0.5
    y = 4.5 - int(state / 5)
    mouse_img.set_extent((x - 0.3, x + 0.3, y - 0.3, y + 0.3))
    return (mouse_img,)

# 치즈 이미지를 출력합니다
ax.imshow(cheese_img, extent=(4.3, 4.7, 0.3, 0.7), zorder=1)

# 초기화 함수와 애니메이션 생성 함수를 사용하여 애니메이션을 생성합니다
anim = animation.FuncAnimation(fig, animate, init_func=init, frames=len(state_history), interval=100, repeat=False)

# 그림을 표시합니다
plt.show()
