import React, { FC } from 'react';
import styled from 'styled-components';
import axios from 'axios';
import { Form, Input, Button, Checkbox } from 'antd';
import logo from '../../assets/logo.svg';
import '../../App.css';

const Login: FC = () => {
  const onFinish = (values: any) => {
    console.log(values);

    // axios.post('http://localhost:7151/api/User/register');
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
            labelCol={{ span: 8 }}
            wrapperCol={{ span: 16 }}
            initialValues={{ remember: true }}
            onFinish={onFinish}
            onFinishFailed={onFinishFailed}
            autoComplete="off"
          >
            <Form.Item
              label="Username"
              name="username"
              rules={[
                { required: true, message: 'Please input your username!' },
              ]}
            >
              <Input />
            </Form.Item>

            <Form.Item
              label="Password"
              name="password"
              rules={[
                { required: true, message: 'Please input your password!' },
              ]}
            >
              <Input.Password />
            </Form.Item>

            <Form.Item
              name="remember"
              valuePropName="checked"
              wrapperCol={{ offset: 8, span: 16 }}
            >
              <Checkbox>Remember me</Checkbox>
            </Form.Item>

            <Form.Item wrapperCol={{ offset: 8, span: 16 }}>
              <Button type="primary" htmlType="submit">
                Submit
              </Button>
            </Form.Item>
          </StyledForm>
        </StyledFormWrapper>
      </StyledLoginContent>
      <StyledImageWrapper>
        <StyledImage src={logo} alt="no-image" />
      </StyledImageWrapper>
    </StyledPage>
  );
};

export default Login;

const StyledPage = styled.div`
  display: flex;
  height: 100vh;
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
  padding: 40px;
  border-radius: 12px;
  border: 1px solid black;
  justify-content: center;
  align-items: center;
`;
const StyledForm = styled(Form)``;

const StyledImage = styled.img`
  height: 100%;
  width: 100%;
`;
