import { FC } from 'react';
import styled from 'styled-components';
import { Form, Input, Button } from 'antd';
import { ValidateErrorEntity } from 'rc-field-form/lib/interface';
import '../../App.css';

interface ILoginFormProps {
  onSubmit: (values: any) => void;
  onFailed: ((errorInfo: ValidateErrorEntity<unknown>) => void) | undefined;
}

const RegisterForm: FC<ILoginFormProps> = ({
  onSubmit,
  onFailed = undefined,
}) => {
  return (
    <>
      <StyledHeader>SignUp</StyledHeader>
      <StyledForm
        id="registerForm"
        name="basic"
        labelCol={{ span: 8 }}
        wrapperCol={{ span: 16 }}
        initialValues={{ remember: true }}
        onFinish={onSubmit}
        onFinishFailed={onFailed}
        autoComplete="off"
      >
        <Form.Item
          label="Firstname"
          name="name"
          rules={[{ required: true, message: 'Please input your first name' }]}
        >
          <Input />
        </Form.Item>

        <Form.Item
          label="Lastname"
          name="lastname"
          rules={[{ required: true, message: 'Please input your last name' }]}
        >
          <Input />
        </Form.Item>

        <Form.Item
          label="Email"
          name="email"
          rules={[{ required: true, message: 'Please input your username' }]}
        >
          <Input />
        </Form.Item>

        <Form.Item
          label="Password"
          name="password"
          rules={[{ required: true, message: 'Please input your password' }]}
        >
          <Input.Password />
        </Form.Item>

        <Form.Item
          label="Confirm password"
          name="password"
          rules={[{ required: true, message: 'Please confirm your password' }]}
        >
          <Input.Password />
        </Form.Item>

        <Form.Item
          label="Department"
          name="department"
          rules={[{ required: true, message: 'Please input your department' }]}
        >
          <Input />
        </Form.Item>

        <Form.Item
          label="Position"
          name="position"
          rules={[{ required: true, message: 'Please input your position' }]}
        >
          <Input />
        </Form.Item>
      </StyledForm>
      <StyledLoginButton type="primary" htmlType="submit" form="registerForm">
        Register
      </StyledLoginButton>
    </>
  );
};

export default RegisterForm;

const StyledLoginButton = styled(Button)`
  border-radius: 8px;

  &:hover,
  &:focus {
    box-shadow: 1px 2px 4px 1px rgba(0, 0, 0, 0.1);
    background-color: #47a6ff;
  }
`;

const StyledHeader = styled.h1`
  font-size: 32px;
  color: #ffffff;
  opacity: 0.9;
`;

const StyledForm = styled(Form)`
  width: auto;
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
