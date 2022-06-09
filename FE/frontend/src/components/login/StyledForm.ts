import styled from 'styled-components';
import { Input, Button } from 'antd';

import landingImage from '../../assets/landing.jpg';

export const StyledPage = styled.div`
  display: flex;
  flex-direction: column;
  justify-content: center;
  height: 100vh;
  background-image: url(${landingImage});
  background-size: cover;
  background-position: center;
  background-repeat: no-repeat;
`;

export const StyledFormWrapper = styled.div`
  display: flex;
  flex-direction: column;
  padding: 3rem 0;
  max-width: 40rem;
  margin: 0 16rem;
  border-radius: 12px;
  box-shadow: -1px 1px 10px -1px #ffffff;
  justify-content: center;
  align-items: center;
  background-image: linear-gradient(
    to left bottom,
    rgba(92, 159, 231, 0.1),
    rgba(221, 233, 246, 0.9)
  );
`;

export const StyledLoginButton = styled(Button)`
  border-radius: 8px;
  /* margin-top: 3rem; */

  &:hover,
  &:focus {
    box-shadow: 1px 2px 4px 1px rgba(0, 0, 0, 0.1);
    background-color: #47a6ff;
    color: #fff;
  }
`;

export const StyledHeader = styled.h1`
  font-size: 32px;
  color: #ffffff;
  opacity: 0.9;
`;

export const StyledForm = styled.form`
  display: flex;
  flex-direction: column;
  gap: 1.2rem;
  width: 32rem;

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

export const StyledFormInput = styled(Input)`
  background-color: #fff;
`;

export const StyledFormBox = styled.div`
  display: flex;
  flex-direction: column;
  position: relative;
`;

export const StyledFormSpan = styled.span`
  position: absolute;
  bottom: -2rem;
  left: 50%;
  transform: translateX(-50%);
  color: red;
`;

export const StyledToggleButton = styled(Button)`
  border-radius: 8px;
  margin-top: 1.3rem;
  color: #777;
  background: #fcfcfc;
  border: none;
  outline: none;

  &:hover,
  &:focus,
  &:active {
    color: #777;
    box-shadow: 1px 2px 6px 1px rgba(0, 0, 0, 0.1);
  }
`;
