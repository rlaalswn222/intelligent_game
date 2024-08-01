# 구현에 사용할 패키지 임포트
import numpy as np
import matplotlib.pyplot as plt

# 초기 상태의 미로 모습

# 전체 그림의 크기 및 그림을 나타내는 변수 선언
fig = plt.figure(figsize=(6, 6))
ax = plt.gca()
ax.set_xlim(0, 5)
ax.set_ylim(0, 5)
plt.tick_params(axis='both', which='both', bottom=False, top=False, labelbottom=False, right=False, left=False, labelleft=False)

# 상태 Start와 Goal Point표시
plt.text(0.5, 1.5, 'START', ha='center')
plt.text(4.5, 2.5, 'GOAL', ha='center')
plt.plot([])

# 상태를 의미하는 문자열(S0~S8) 표시
plt.text(0.5, 0.5, '-10.00', size=14, ha='center')
plt.text(1.5, 0.5, '-10.00', size=14, ha='center')
plt.text(2.5, 0.5, '-10.00', size=14, ha='center')
plt.text(3.5, 0.5, '-10.00', size=14, ha='center')
plt.text(4.5, 0.5, '-10.00', size=14, ha='center')

# 각 포인트의 position x, y를 리스트로 저장
point_position_list = [[0.5, 4.5], [1.5, 4.5], [2.5, 4.5], [3.5, 4.5], [4.5, 4.5],
                       [0.5, 3.5], [1.5, 3.5], [2.5, 3.5], [3.5, 3.5], [4.5, 3.5],
                       [0.5, 2.5], [1.5, 2.5], [2.5, 2.5], [3.5, 2.5], [4.5, 2.5],
                       [0.5, 1.5], [1.5, 1.5], [2.5, 1.5], [3.5, 1.5], [4.5, 1.5],
                       [0.5, 0.5], [1.5, 0.5], [2.5, 0.5], [3.5, 0.5], [4.5, 0.5]]

# 포인트의 인덱스를 미리 저장
start_point = 15
goal_point = 14
wall_point_list = [6, 7, 8, 11, 12, 13]
cliff_point_list = [20, 21, 22, 23, 24]
normal_point_list = [0, 1, 2, 3, 4,
                     5, 9,
                     10,
                     16, 17, 18, 19]

# 현재 위치를 표시
line, = ax.plot([0.5], [1.5], marker="o", color='g', markersize=90)

#미로 보기
#plt.show()

# 정책을 결정하는 파라미터의 초깃값 theta_0를 설정
# 줄은 상태 0~7, 열은 행동방향(상,우,하,좌 순)를 나타낸다
theta_0 = np.array([[np.nan, 1, 1, np.nan],
                    [np.nan, 1, np.nan, 1],
                    [np.nan, 1, np.nan, 1],
                    [np.nan, 1, np.nan, 1],
                    [np.nan, np.nan, 1, 1],
                    
                    [1, np.nan, 1, np.nan],
                    [np.nan, np.nan, np.nan, np.nan],
                    [np.nan, np.nan, np.nan, np.nan],
                    [np.nan, np.nan, np.nan, np.nan],
                    [1, np.nan, 1, np.nan],

                    [1, np.nan, 1, np.nan],
                    [np.nan, np.nan, np.nan, np.nan],
                    [np.nan, np.nan, np.nan, np.nan],
                    [np.nan, np.nan, np.nan, np.nan],
                    [np.nan, np.nan, np.nan, np.nan], # 목표 지점

                    [1, 1, 1, np.nan], # 시작 지점
                    [np.nan, 1, 1, 1],
                    [np.nan, 1, 1, 1],
                    [np.nan, 1, 1, 1],
                    [1, np.nan, 1, 1],

                    [1, 1, np.nan, np.nan],
                    [1, 1, np.nan, 1],
                    [1, 1, np.nan, 1],
                    [1, 1, np.nan, 1],
                    [1, np.nan, np.nan, 1],
                    ])

# Q 값을 초기화
Q = np.random.rand(25, 4)  # 모든 상태-행동 쌍에 대해 Q 값을 임의로 초기화
Q = np.where(np.isnan(theta_0), np.nan, Q)  # theta_0에서 nan인 부분은 Q에서도 nan으로 설정

# 정책 파라미터 theta_0을 무작위 행동 정책 pi로 변환하는 함수
def simple_convert_into_pi_from_theta(theta):
    [m, n] = theta.shape  # theta의 행렬 크기를 구함
    pi = np.zeros((m, n))
    for i in range(0, m):
        if np.nansum(theta[i, :]) == 0:
            pi[i, :] = 0
        else:
            pi[i, :] = theta[i, :] / np.nansum(theta[i, :])  # 비율 계산

    pi = np.nan_to_num(pi)  # nan을 0으로 변환

    return pi

# 무작위 행동정책 pi_0을 계산
pi_0 = simple_convert_into_pi_from_theta(theta_0)

# ε-greedy 알고리즘 구현
def get_action(s, Q, epsilon, pi_0):
    direction = ["up", "right", "down", "left"]

    # 행동을 결정
    if np.random.rand() < epsilon:
        # 확률 ε로 무작위 행동을 선택함
        next_direction = np.random.choice(direction, p=pi_0[s, :])
    else:
        # Q값이 모두 nan인 경우를 처리
        if np.all(np.isnan(Q[s, :])):
            next_direction = np.random.choice(direction, p=pi_0[s, :])
        else:
            # Q값이 최대가 되는 행동을 선택함
            next_direction = direction[np.nanargmax(Q[s, :])]

    # 행동을 인덱스로 변환
    if next_direction == "up":
        action = 0
    elif next_direction == "right":
        action = 1
    elif next_direction == "down":
        action = 2
    elif next_direction == "left":
        action = 3

    return action

# 목표에 도달할 때까지 Q 값을 갱신하는 함수
def goal_maze_ret_s_a_Q(Q, epsilon, eta, gamma, pi):
    s = start_point  # 초기 상태
    a = get_action(s, Q, epsilon, pi)  # 초기 행동

    s_a_history = [[s, a]]  # 에이전트의 상태와 행동의 기록

    while (1):  # 목표에 도달할 때까지 반복
        s_next = get_next_state(s, a)
        if s_next == goal_point:  # 목표 지점에 도달하면 종료
            r = 1  # 보상
            Q[s, a] = Q[s, a] + eta * (r + gamma * 0 - Q[s, a])
            break
        elif s_next in cliff_point_list:  # 절벽 포인트에 도달하면
            r = -10  # 보상
            Q[s, a] = Q[s, a] + eta * (r + gamma * 0 - Q[s, a])
            break
        else:
            r = -0.04  # 보상
            a_next = get_action(s_next, Q, epsilon, pi)  # 다음 행동
            Q[s, a] = Q[s, a] + eta * (r + gamma * Q[s_next, a_next] - Q[s, a])

            s = s_next
            a = a_next
            s_a_history.append([s, a])

    return s_a_history, Q

# 다음 상태를 결정하는 함수
def get_next_state(s, a):
    direction = ["up", "right", "down", "left"]

    next_state = s

    if direction[a] == "up":
        next_state = s - 5  # 위로 이동
    elif direction[a] == "right":
        next_state = s + 1  # 오른쪽으로 이동
    elif direction[a] == "down":
        next_state = s + 5  # 아래로 이동
    elif direction[a] == "left":
        next_state = s - 1  # 왼쪽으로 이동

    # 이동할 위치가 미로의 범위를 벗어나면 현재 상태 유지
    if next_state < 0 or next_state >= 25:
        next_state = s
    if next_state in wall_point_list:
        next_state = s

    return next_state

# Q러닝 알고리즘으로 미로 빠져나오기
eta = 0.1  # 학습률
gamma = 0.9  # 시간할인율
epsilon = 0.5  # ε-greedy 알고리즘 epsilon 초깃값
v = np.nanmax(Q, axis=1)  # 각 상태마다 가치의 최댓값을 계산
is_continue = True
episode = 1

V = []  # 에피소드 별로 상태가치를 저장
V.append(np.nanmax(Q, axis=1))  # 상태 별로 행동가치의 최댓값을 계산

while is_continue:  # is_continue의 값이 False가 될 때까지 반복
    print("에피소드: " + str(episode))

    # ε 값을 조금씩 감소시킴
    epsilon = epsilon / 2

    # Q러닝으로 미로를 빠져나온 후, 결과로 나온 행동 히스토리와 Q값을 변수에 저장
    [s_a_history, Q] = goal_maze_ret_s_a_Q(Q, epsilon, eta, gamma, pi_0)

    # 상태가치의 변화
    new_v = np.nanmax(Q, axis=1)  # 각 상태마다 행동가치의 최댓값을 계산
    print(np.sum(np.abs(new_v - v)))  # 상태가치 함수의 변화를 출력
    v = new_v
    V.append(v)  # 현재 에피소드가 끝난 시점의 상태가치 함수를 추가

    print("목표 지점에 이르기까지 걸린 단계 수는 " + str(len(s_a_history) - 1) + "단계입니다")

    # 100 에피소드 반복
    episode = episode + 1
    if episode > 100:
        break

# 상태가치의 변화를 시각화
import matplotlib.animation as animation
import matplotlib.cm as cm  # color map

def init():
    # 배경색 초기화
    line.set_data([], [])
    return (line,)

def animate(i):
    # 프레임 단위로 그림을 그림
    # 각 칸에 상태가치 값으로 결정된 색을 칠함
    for j in range(len(point_position_list)):
        line, = ax.plot(point_position_list[j][0], point_position_list[j][1], marker="s", 
                        color=cm.jet(V[i][j]), markersize=50)
    for j in wall_point_list:
        line, = ax.plot(point_position_list[j][0], point_position_list[j][1], marker = "s", 
                        color = "gray", markersize = 50)
        
    line, = ax.plot(point_position_list[goal_point][0], 
                    point_position_list[goal_point][1], marker="s", color=cm.jet(1.0), markersize=50)

    return (line,)

# 초기화 함수와 프레임 단위로 그림을 그리는 함수로 애니메이션을 생성
anim = animation.FuncAnimation(
    fig, animate, init_func=init, frames=len(V), interval=200, repeat=False)

plt.show()