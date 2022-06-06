import { FC, useRef } from 'react';
import { Form, Input, Button } from 'antd';
import { ValidateErrorEntity } from 'rc-field-form/lib/interface';
import styled from 'styled-components';
import '../../App.css';
import { useForm, Controller, SubmitHandler } from 'react-hook-form';
import { yupResolver } from '@hookform/resolvers/yup';
import { loginSchema } from '../../util/validation-schema/ValidationSchema';

interface ILoginFormProps {
  onSubmit: (values: unknown) => void;
  onFailed: ((errorInfo: ValidateErrorEntity<unknown>) => void) | undefined;
}

type IFormValues = {
  email: string;
  password: string;
};

const LoginForm: FC<ILoginFormProps> = ({ onSubmit, onFailed = undefined }) => {
  const {
    control,
    handleSubmit,
    formState: { errors },
  } = useForm<IFormValues>({
    resolver: yupResolver(loginSchema),
  });

  return (
    <>
      <StyledHeader>Login</StyledHeader>
      <StyledForm id="loginForm" onSubmit={handleSubmit(onSubmit)}>
        <Controller
          name="email"
          control={control}
          defaultValue=""
          render={({ field }) => (
            <div>
              <span>Email:</span>
              <StyledFormInput {...field} />
            </div>
          )}
        />

        <Controller
          name="password"
          control={control}
          defaultValue=""
          render={({ field }) => (
            <div>
              <span>Password:</span>
              <StyledFormInput type="password" {...field} />
            </div>
          )}
        />
      </StyledForm>
      <StyledLoginButton type="primary" htmlType="submit" form="loginForm">
        Login
      </StyledLoginButton>
    </>
    // <form onSubmit={handleSubmit(onSubmit)}>
    //   <label htmlFor="email">Email:</label>
    //   <input
    //     {...register('email', { required: 'This field is required' })}
    //     id="email"
    //   />
    //   <label htmlFor="password">Password</label>
    //   <input
    //     {...register('password', { required: 'This field is required' })}
    //     id="password"
    //   />
    //   <button type="submit">Login</button>
    // </form>
    // <>
    //   <StyledHeader>Login</StyledHeader>
    //   <StyledForm
    //     name="basic"
    //     id="loginForm"
    //     labelCol={{ span: 8 }}
    //     wrapperCol={{ span: 16 }}
    //     initialValues={{ remember: true }}
    //     onFinish={onSubmit}
    //     onFinishFailed={onFailed}
    //     autoComplete="off"
    //   >
    // <Form.Item
    //   label="Email"
    //   name="email"
    //   rules={[{ required: true, message: 'Please input your username' }]}
    // >
    //   <Input />
    // </Form.Item>

    //     <Form.Item
    //       label="Password"
    //       name="password"
    //       rules={[{ required: true, message: 'Please input your password' }]}
    //     >
    //       <Input.Password />
    //     </Form.Item>
    //   </StyledForm>
    //   <StyledLoginButton type="primary" htmlType="submit" form="loginForm">
    //     Login
    //   </StyledLoginButton>
    // </>
  );
};

export default LoginForm;

const StyledLoginButton = styled(Button)`
  border-radius: 8px;
  margin-top: 1.5rem;

  &:hover,
  &:focus {
    box-shadow: 1px 2px 4px 1px rgba(0, 0, 0, 0.1);
    background-color: #47a6ff;
    color: #fff;
  }
`;

const StyledHeader = styled.h1`
  font-size: 32px;
  color: #ffffff;
  opacity: 0.9;
`;

const StyledForm = styled.form`
  & input,
  & span {
    opacity: 0.75;
    border-radius: 10px;
  }

  & input:active,
  & input:focus {
    opacity: 1;
  }
`;

const StyledFormInput = styled(Input)`
  background-color: #fff;
`;
