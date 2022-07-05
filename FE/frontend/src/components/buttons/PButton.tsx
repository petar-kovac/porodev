import { FC, MouseEventHandler, ReactNode } from 'react';
import { Button } from 'antd';
import { PoweroffOutlined } from '@ant-design/icons';
import styled from 'styled-components';
import theme from 'theme/theme';

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
  isLoading?: boolean;
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
  isLoading,
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
      loading={isLoading}
    >
      {text}
    </StyledButton>
  );
};

const StyledButton = styled(Button).attrs((props) => ({
  'data-testid': `${props.form}-button`,
}))<IPButtonProps>`
  color: ${(props) => (props.color ? props.color : '#fff')};
  border-radius: ${(props) => (props.radius ? props.radius : '12px')};
  border: none;
  outline: none;
  background-color: ${(props) =>
    props.background ? props.background : theme.colors.primary};

  &:hover,
  &:focus,
  &:active {
    color: ${(props) => (props.color ? props.color : '#fff')};
    box-shadow: 1px 2px 6px 1px rgba(0, 0, 0, 0.1);
    background-color: ${(props) =>
      props.background
        ? `${props.background}bf`
        : `${theme.colors.primary}bf`}; // simulating hover effect, this function is adding a little bit of opacity
  }
`;

export default PButton;
