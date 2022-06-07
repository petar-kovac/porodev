import { message } from 'antd';
import * as yup from 'yup';

const emailRegex = /^[a-zA-Z0-9.-_]+@boing\.rs$/g;
const passwordRegex =
  /^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^\w\s]).{8,}$/g;

export const registrationSchema = yup.object().shape({
  firstName: yup.string().required('First name is required'),
  lastName: yup.string().required('Last name is required'),
  email: yup
    .string()
    .required('Email is required')
    .matches(emailRegex, 'Email is invalid'),
  password: yup
    .string()
    .required('Password is required')
    .matches(passwordRegex, 'Wrong password'),
  confirmPassword: yup
    .string()
    .required('Confirm password')
    .oneOf([yup.ref('password'), null]),
  department: yup
    .number()
    .positive()
    .integer()
    .required('Department is required'),
  position: yup.string().required('Position is required'),
});

export const loginSchema = yup.object().shape({
  email: yup
    .string()
    .required('Email is required')
    .matches(emailRegex, 'Invalid email'),
  password: yup
    .string()
    .required('Password is required')
    .matches(passwordRegex, 'Wrong password'),
});
