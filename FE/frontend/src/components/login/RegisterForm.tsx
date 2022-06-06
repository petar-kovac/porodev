import { FC } from 'react';
import styled from 'styled-components';
import { Form, Input, Button } from 'antd';
import { ValidateErrorEntity } from 'rc-field-form/lib/interface';
import '../../App.css';
import { useForm, Controller } from 'react-hook-form';
import { yupResolver } from '@hookform/resolvers/yup';
import { registrationSchema } from '../../util/validation-schema/ValidationSchema';

interface ILoginFormProps {
  onSubmit: (values: unknown) => void;
  onFailed: ((errorInfo: ValidateErrorEntity<unknown>) => void) | undefined;
}

type FormValues = {
  firstName: string;
  lastName: string;
  email: string;
  password: string;
  confirmPassword: string;
  department: number;
  position: string;
};

const RegisterForm: FC<ILoginFormProps> = ({
  onSubmit,
  onFailed = undefined,
}) => {
  const {
    control,
    handleSubmit,
    formState: { errors },
  } = useForm<FormValues>({
    resolver: yupResolver(registrationSchema),
  });

  return (
    <>
      <StyledHeader>SignUp</StyledHeader>
      <StyledForm id="registerForm" onSubmit={handleSubmit(onSubmit)}>
        <Controller
          name="firstName"
          control={control}
          defaultValue=""
          render={({ field }) => (
            <div>
              <span>First name:</span>
              <StyledFormInput {...field} />
            </div>
          )}
        />

        <Controller
          name="lastName"
          control={control}
          defaultValue=""
          render={({ field }) => (
            <div>
              <span>Last name:</span>
              <StyledFormInput {...field} />
            </div>
          )}
        />

        <Controller
          name="email"
          control={control}
          defaultValue=""
          render={({ field }) => (
            <div>
              <span>E-mail:</span>
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
              <StyledFormInput
                {...field}
                type="password"
                autoComplete="new-password"
              />
            </div>
          )}
        />

        <Controller
          name="confirmPassword"
          control={control}
          defaultValue=""
          render={({ field }) => (
            <div>
              <span>Confirm password:</span>
              <StyledFormInput {...field} type="password" />
            </div>
          )}
        />

        <Controller
          name="department"
          control={control}
          render={({ field }) => (
            <div>
              <span>Department:</span>
              <StyledFormInput {...field} />
            </div>
          )}
        />

        <Controller
          name="position"
          control={control}
          defaultValue=""
          render={({ field }) => (
            <div>
              <span>Position:</span>
              <StyledFormInput {...field} />
            </div>
          )}
        />
      </StyledForm>
      <StyledLoginButton type="primary" htmlType="submit" form="registerForm">
        Register
      </StyledLoginButton>
    </>
    // <form onSubmit={handleSubmit(onSubmit)} autoComplete="off">
    //   <label htmlFor="firstName">First name:</label>
    //   <input type="text" {...register('firstName')} id="firstName" />

    //   <label htmlFor="lastName">Last name:</label>
    //   <input type="text" {...register('lastName')} id="lastName" />

    //   <label htmlFor="email">Email:</label>
    //   <input type="text" {...register('email')} id="email" />

    //   <label htmlFor="password">Password:</label>
    //   <input type="password" {...register('password')} id="password" />

    //   <label htmlFor="confirmPassword">Confirm password:</label>
    //   <input
    //     type="password"
    //     {...register('confirmPassword')}
    //     id="confirmPassword"
    //   />

    //   <label htmlFor="department">Department:</label>
    //   <input type="text" {...register('department')} id="department" />

    //   <label htmlFor="position">Position:</label>
    //   <input type="text" {...register('position')} id="position" />

    //   <button type="submit">Register</button>
    // </form>

    // <>
    //   <StyledHeader>SignUp</StyledHeader>
    //   <StyledForm
    //     id="registerForm"
    //     name="basic"
    //     labelCol={{ span: 8 }}
    //     wrapperCol={{ span: 16 }}
    //     initialValues={{ remember: true }}
    //     onFinish={onSubmit}
    //     onFinishFailed={onFailed}
    //     autoComplete="off"
    //   >
    //     <Form.Item
    //       label="Firstname"
    //       name="name"
    //       rules={[{ required: true, message: 'Please input your first name' }]}
    //     >
    //       <Input />
    //     </Form.Item>

    //     <Form.Item
    //       label="Lastname"
    //       name="lastname"
    //       rules={[{ required: true, message: 'Please input your last name' }]}
    //     >
    //       <Input />
    //     </Form.Item>

    //     <Form.Item
    //       label="Email"
    //       name="email"
    //       rules={[{ required: true, message: 'Please input your username' }]}
    //     >
    //       <Input />
    //     </Form.Item>

    //     <Form.Item
    //       label="Password"
    //       name="password"
    //       rules={[{ required: true, message: 'Please input your password' }]}
    //     >
    //       <Input.Password />
    //     </Form.Item>

    //     <Form.Item
    //       label="Confirm password"
    //       name="password"
    //       rules={[{ required: true, message: 'Please confirm your password' }]}
    //     >
    //       <Input.Password />
    //     </Form.Item>

    //     <Form.Item
    //       label="Department"
    //       name="department"
    //       rules={[{ required: true, message: 'Please input your department' }]}
    //     >
    //       <Input />
    //     </Form.Item>

    //     <Form.Item
    //       label="Position"
    //       name="position"
    //       rules={[{ required: true, message: 'Please input your position' }]}
    //     >
    //       <Input />
    //     </Form.Item>
    //   </StyledForm>
    //   <StyledLoginButton type="primary" htmlType="submit" form="registerForm">
    //     Register
    //   </StyledLoginButton>
    // </>
  );
};

export default RegisterForm;

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
