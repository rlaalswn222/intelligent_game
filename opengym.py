import gym
import numpy as np
# CartPole-v1 환경 : v0, v1,.. 버전에 따라 다르므로 version에 주의

ENV = 'CartPole-v0' # 태스크 이름
NUM_DIGITIZED = 6 # 각 상태를 이산 변수로 변환할 구간 수
GAMMA = 0.99 # 시간 할인율
ETA = 0.5 # 학습률
MAX_STEPS = 500 # 1 episode 당 최대 단계 수
NUM_EPISODES = 1000 # 최대 에피소드 수

n=500
env = gym.make("Pendulum-v1", render_mode="human") #openAI 게임 실행
observation, info = env.reset(seed=82) #무작위 난수를 같게 생성, 잘 나오는지 테스트 가능

# 무작위 밸런싱 구현: 무작위로 왼쪽 또는 오른쪽 action 선택
for _ in range(n) :
    action = env.action_space.sample()
    observation, reward, terminated, truncated, info = env.step(action)
    print("observation : ", observation)
    # print [cart_pos, cart_v, pole_angle, pole_v]

    if terminated or truncated:
        observation, info = env.reset()
env.close()