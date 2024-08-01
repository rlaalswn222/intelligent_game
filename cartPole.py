import numpy as np
import gym

# 상수 정의
ENV = 'CartPole-v0' # 태스크 이름
NUM_DIGITIZED = 6 # 각 상태를 이산 변수로 변환할 구간 수
GAMMA = 0.99 # 시간 할인율
ETA = 0.5 # 학습률
MAX_STEPS = 500 # 1 episode 당 최대 단계 수
NUM_EPISODES = 100 # 최대 에피소드 수


class Agent:
    def __init__(self, num_states, num_actions):
        self.brain = Brain(num_states, num_actions) # 태스크의 상태 변수와 행동 수 설정
    
    def get_action(self, observation, episode):
        action = self.brain.decide_action(observation, episode)
        return action
    
    def update_Q_function(self, observation, action, reward, observation_next):
        self.brain.update_Q_table(observation, action, reward, observation_next)

class Brain:
    '''에이전트의 두뇌 역할을 하는 클래스, Q러닝을 실제 수행'''
    def __init__(self, num_states, num_actions):
        self.num_actions = num_actions # 행동의 가짓수(왼쪽, 오른쪽)를 구현
        
        # Q테이블을 생성. 줄 수는 상태를 구간수^4(4는 변수의 수)가지 값 중
        # 하나로 변환한 값, 열 수는 행동의 가짓수,
        # 2차원 배열(size 크기)을 random(0~1) 값으로 초기화
        self.q_table = np.random.uniform(low=-1, high=1, size=(NUM_DIGITIZED**num_states, num_actions))
    
    def bins(self, clip_min, clip_max, num):
        '''관측된 상태(연속값)를 이산변수로 변환하는 구간을 계산'''
        return np.linspace(clip_min, clip_max, num + 1)[1:-1]
    
    def digitize_state(self, observation):
        '''관측된 상태 observation을 이산변수로 변환'''
        cart_pos, cart_v, pole_angle, pole_v = observation
        digitized = [
            np.digitize(cart_pos, bins=self.bins(-2.4, 2.4, NUM_DIGITIZED)),
            np.digitize(cart_v, bins=self.bins(-3.0, 3.0, NUM_DIGITIZED)),
            np.digitize(pole_angle, bins=self.bins(-0.5, 0.5, NUM_DIGITIZED)),
            np.digitize(pole_v, bins=self.bins(-2.0, 2.0, NUM_DIGITIZED))
        ]
        return sum([x * (NUM_DIGITIZED**i) for i, x in enumerate(digitized)])
    
    def update_Q_table(self, observation, action, reward, observation_next):
        '''Q러닝으로 Q테이블을 수정'''
        state = self.digitize_state(observation)
        # 상태를 이산변수로 변환
        state_next = self.digitize_state(observation_next)
        # 다음 상태를 이산변수로 변환
        max_q_next = max(self.q_table[state_next][:])
        self.q_table[state, action] = self.q_table[state, action] + ETA * (reward + GAMMA * max_q_next - self.q_table[state, action])
        #state가 연속값, 4개를 이산화 시켜서 넣어주려 하는 거
    
    def decide_action(self, observation, episode):
        '''ε-greedy 알고리즘을 적용하여 서서히 최적행동의 비중을 늘림'''
        state = self.digitize_state(observation)
        epsilon = 0.5 * (1 / (episode + 1))

        if epsilon <= np.random.uniform(0, 1):
            state = self.digitize_state(observation)
            action = np.argmax(self.q_table[state][:])
        else:
            action = np.random.choice(self.num_actions)
        return action

class Environment:
    '''CartPole을 실행하는 환경 역할을 하는 클래스'''

    def __init__(self):
        self.env = gym.make(ENV, render_mode="human") # 실행할 태스크 설정
        self.env.action_space.seed(82)

        num_states = self.env.observation_space.shape[0] # 태스크의 상태 변수 수를 구함
        num_actions = self.env.action_space.n # 가능한 행동 수를 구함
        self.agent = Agent(num_states, num_actions) # 에이전트 객체를 생성


    def run(self):
        '''실행'''
        complete_episodes = 0 # 성공한(195단계 이상 버틴) 에피소드 수
        is_episode_final = False # 마지막 에피소드 여부

        for episode in range(NUM_EPISODES): # 에피소드 수 만큼 반복
            observation, info = self.env.reset(seed=82) # 환경 초기화
            print("observation : ", observation)

            for step in range(MAX_STEPS): # 1 에피소드에 해당하는 반복, 들어갈 때마다 max step 리셋
                # 행동을 선택
                action= self.agent.get_action(observation, episode) # Agent class
                
                # 행동 a_t를 실행하여 s_{t+1}, r_{t+1}을 계산
                observation_next, _, done, _, _ = self.env.step(action)
                # reward, info는 사용하지 않으므로


                # 보상 부여
                if done: # 200단계를 넘어서거나 일정 각도 이상 기울면 done의 값이 True가 됨
                    if step < 195: #195를 넘지 못하는건 패널티
                        reward= -1 # 봉이 쓰러지면 페널티로 보상-1 부여
                        complete_episodes = 0 # 195단계 이상 버티면 해당 에피소드 성공 처리
                    else:
                        reward= 1 # 쓰러지지 않고 에피소드를 끝내면 보상 1 부여
                        complete_episodes += 1 # 에피소드 연속 성공 기록을 업데이트
                else:
                    reward= 0 # 에피소드 중에는 보상이 0, done전에는 보상 없음

                # 다음 단계의 상태 observation_next로 Q함수 수정
                self.agent.update_Q_function(observation, action, reward, observation_next)
                # Agent class

                # 다음 단계 상태 관측
                observation = observation_next

                # 에피소드 마무리
                if done:
                    print('{0} Episode: Finished after {1} time steps'.format(episode, step + 1))
                    break
                #몇번째 에피소드가 어디까지 스텝이라고 끝냄

            # 에피소드 완료 이후
            if complete_episodes >= 10: # 10 에피소드 연속으로 성공한 경우
                print('10 에피소드 연속 성공')
                is_episode_final = True #다음 에피소드가 마지막 에피소드
                break

        if is_episode_final:
            print("학습 완료됐습니다.")

if __name__ == '__main__':
    env = Environment()
    env.run()