import { Button } from 'antd';
import { FC, MouseEventHandler } from 'react';
import styled from 'styled-components';

interface IPButtonProps {
  text?: string;
  color?: string;
  borderRadius?: string;
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
  onClick?: MouseEventHandler<HTMLButtonElement> | undefined;
}

const PButton: FC<IPButtonProps> = ({
  text,
  color,
  borderRadius,
  background,
  htmlType,
  form,
  type,

  onClick,
}) => {
  return (
    <StyledButton
      color={color}
      borderRadius={borderRadius}
      background={background}
      htmlType={htmlType}
      form={form}
      type={type}
      onClick={onClick}
    >
      {text}
    </StyledButton>
  );
};

const StyledButton = styled(Button).attrs((props) => ({
  'data-testid': `${props.form}-button`,
}))<IPButtonProps>`
  color: ${(props) => props.color};
  border-radius: ${(props) => props.borderRadius};
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
