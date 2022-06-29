import { FC, MouseEventHandler, ReactNode } from 'react';
import { Button } from 'antd';
import { PoweroffOutlined } from '@ant-design/icons';
import styled from 'styled-components';

interface IPButtonProps {
  text?: string;
  color?: string;
  radius?: string;
  background?: string;
  htmlType?: 'button' | 'submit' | 'reset' | undefined;
  form?: string;
  type?:
    | 'link'
    | 'text'
    | 'default'
    | 'ghost'
    | 'primary'
    | 'dashed'
    | undefined;
  icon?: ReactNode;
  onClick?: MouseEventHandler<HTMLButtonElement> | undefined;
}

const PButton: FC<IPButtonProps> = ({
  text,
  color,
  radius,
  background,
  htmlType,
  form,
  type,
  icon,
  onClick,
}) => {
  return (
    <StyledButton
      color={color}
      background={background}
      radius={radius}
      htmlType={htmlType}
      form={form}
      type={type}
      onClick={onClick}
      icon={icon}
    >
      {text}
    </StyledButton>
  );
};

const StyledButton = styled(Button).attrs((props) => ({
  'data-testid': `${props.form}-button`,
}))<IPButtonProps>`
  color: ${(props) => props.color};
  border-radius: ${(props) => props.radius};
  border: none;
  outline: none;
  background-color: ${(props) => props.background};

  &:hover,
  &:focus,
  &:active {
    color: ${(props) => props.color};
    box-shadow: 1px 2px 6px 1px rgba(0, 0, 0, 0.1);
    background-color: ${(props) =>
      `${props.background}bf`}; // simulating hover effect, this function is adding a little bit of opacity
  }
`;

export default PButton;
