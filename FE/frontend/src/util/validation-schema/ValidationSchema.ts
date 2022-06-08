import * as yup from 'yup';

const emailRegex = /^[a-zA-Z0-9.-_]+@boing\.rs$/g;
const passwordRegex =
  /^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9])(?=.*?[^\w\s]).{8,}$/g;

export const registrationSchema = yup.object().shape({
  name: yup.string().required('This field is required'),
  lastname: yup.string().required('This field is required'),
  email: yup
    .string()
    .required('This field is required')
    .matches(emailRegex, 'Email is invalid'),
  password: yup
    .string()
    .required('This field is required')
    .matches(passwordRegex, 'Wrong password'),
  confirmPassword: yup
    .string()
    .required('This field is required')
    .oneOf([yup.ref('password'), null], 'Passwords should match'),
  department: yup
    .number()
    .positive()
    .label('department')
    .transform((value, originalValue) => {
      return originalValue === '' ? undefined : value;
    })
    .min(1, 'Must be > 1')
    .typeError('Must be a number')
    .required('This field is required'),
  position: yup.string().required('This field is required'),
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
