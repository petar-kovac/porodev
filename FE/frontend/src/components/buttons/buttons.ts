import { Button } from 'antd';
import styled from 'styled-components';

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
export const StyledFormButton = styled(Button)`
  border-radius: 8px;
  /* margin-top: 3rem; */

  &:hover,
  &:focus {
    box-shadow: 1px 2px 4px 1px rgba(0, 0, 0, 0.1);
    background-color: #47a6ff;
    color: #fff;
  }
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
