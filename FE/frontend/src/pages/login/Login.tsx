import React, { FC, useState } from 'react';
import styled from 'styled-components';
import axios from 'axios';
import { Form, Input, Button, Checkbox } from 'antd';
import landingImage from '../../assets/landing.jpg';
import '../../App.css';

const Login: FC = () => {
  const [isLogin, setIsLogin] = useState<boolean>(true);

  console.log(isLogin, 'lg');
  const onFinish = (values: any) => {
    console.log(values);

    if (isLogin) {
      try {
        const res = async () => {
          axios({
            method: 'post',

            url: 'https://localhost:7151/api/user/login',
            data: {
              ...values,
            },
            headers: {
              'Content-Type': 'application/json; charset=utf-8',
              'Access-Control-Allow-Origin': '*',
            },
          });
        };
        res();
      } catch (err) {
        console.log('ok');
      }
    } else {
      const res = async () => {
        axios({
          method: 'post',
          url: 'https://localhost:7151/api/user/register/User',
          data: {
            ...values,
            avatarUrl: 'url',
          },
          headers: {
            'Content-Type': 'application/json; charset=utf-8',
            'Access-Control-Allow-Origin': '*',
          },
        });
      };
      res();
    }
  };

  const onFinishFailed = (errorInfo: any) => {
    console.log('Failed:', errorInfo);
  };

  return (
    <StyledPage>
      <StyledLoginContent>
        <StyledFormWrapper>
          <StyledForm
            name="basic"
            wide={isLogin}
            labelCol={{ span: 8 }}
            wrapperCol={{ span: 16 }}
            initialValues={{ remember: true }}
            onFinish={onFinish}
            onFinishFailed={onFinishFailed}
            autoComplete="off"
          >
            <StyledHeader>{isLogin ? 'Login' : 'SignUp'}</StyledHeader>
            {!isLogin && (
              <Form.Item
                label="Firstname"
                name="name"
                rules={[
                  { required: true, message: 'Please input your first name' },
                ]}
              >
                <Input />
              </Form.Item>
            )}
            {!isLogin && (
              <Form.Item
                label="Lastname"
                name="lastname"
                rules={[
                  { required: true, message: 'Please input your last name' },
                ]}
              >
                <Input />
              </Form.Item>
            )}
            <Form.Item
              label="Email"
              name="email"
              rules={[
                { required: true, message: 'Please input your username' },
              ]}
            >
              <Input />
            </Form.Item>

            <Form.Item
              label="Password"
              name="password"
              rules={[
                { required: true, message: 'Please input your password' },
              ]}
            >
              <Input.Password />
            </Form.Item>

            {!isLogin && (
              <Form.Item
                label="Confirm password"
                name="password"
                rules={[
                  { required: true, message: 'Please confirm your password' },
                ]}
              >
                <Input.Password />
              </Form.Item>
            )}

            {!isLogin && (
              <Form.Item
                label="Department"
                name="department"
                rules={[
                  { required: true, message: 'Please input your department' },
                ]}
              >
                <Input />
              </Form.Item>
            )}

            {!isLogin && (
              <Form.Item
                label="Position"
                name="position"
                rules={[
                  { required: true, message: 'Please input your position' },
                ]}
              >
                <Input />
              </Form.Item>
            )}

            <Form.Item wrapperCol={{ offset: 8, span: 16 }}>
              <StyledButtonsWrapper>
                <StyledLoginButton type="primary" htmlType="submit">
                  {isLogin ? 'Login' : 'Create account'}
                </StyledLoginButton>
                <StyledChangeStateButton onClick={() => setIsLogin(!isLogin)}>
                  {isLogin
                    ? 'Create new account'
                    : 'Login with existing account'}
                </StyledChangeStateButton>
              </StyledButtonsWrapper>
            </Form.Item>
          </StyledForm>
        </StyledFormWrapper>
      </StyledLoginContent>
      <StyledImageWrapper>
        {/* <StyledImage src={landingImage} alt="no-image" /> */}
      </StyledImageWrapper>
    </StyledPage>
  );
};

export default Login;

const StyledButtonsWrapper = styled.div`
  display: flex;
  flex-direction: column;
`;

const StyledLoginButton = styled(Button)`
  box-shadow: 1px 3px 8px 2px rgba(0, 0, 0, 0.2);
  border-radius: 8px;
`;

const StyledChangeStateButton = styled(Button)`
  background-color: #e6f0fc;
  margin-top: 10px;
  border: none;
  border-radius: 8px;
  box-shadow: 1px 5px 8px 2px rgba(0, 0, 0, 0.15);

  &:hover,
  &:focus {
    color: #515559;
  }
`;

const StyledHeader = styled.h1`
  font-size: 32px;
  color: #ffffff;
  opacity: 0.9;
`;

const StyledPage = styled.div`
  display: flex;
  height: 100vh;
  background-image: url(${landingImage});
  background-size: 100%;
  background-position: center;
  background-repeat: no-repeat;
`;
const StyledLoginContent = styled.div`
  display: flex;
  flex: 0.6;
  justify-content: center;
  align-items: center;
`;
const StyledImageWrapper = styled.div`
  display: flex;
  flex: 1;
`;
const StyledFormWrapper = styled.div`
  display: flex;
  padding: 30px;
  border-radius: 12px;
  border: 1px solid #d4e0f7;
  box-shadow: 0px 3px 8px -1px #ffffff;
  justify-content: center;
  align-items: center;
  background-image: linear-gradient(
    to left bottom,
    rgba(92, 159, 231, 0.1),
    rgba(221, 233, 246, 0.9)
  );
`;
const StyledForm = styled(Form)<{ wide: boolean }>`
  width: ${(props) => (props.wide ? 'auto' : '440px')};
  & input,
  & span {
    opacity: 0.75;
    border-radius: 10px;
  }

  & input:active,
  & input:focus {
    opacity: 1;
  }

  & label {
    color: rgb(0, 0, 0, 0.5);
    font-weight: bold;
  }
`;

const StyledImage = styled.img`
  height: 100%;
  width: 100%;
`;
