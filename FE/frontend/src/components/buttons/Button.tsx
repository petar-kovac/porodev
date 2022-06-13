import { Button } from 'antd';
import { FC } from 'react';
import styled from 'styled-components';

interface IPButtonProps {
  text?: string;
  color?: string;
  borderRadius?: string;
  background?: string;
}

const PButton: FC<IPButtonProps> = ({
  text,
  color,
  borderRadius,
  background,
}) => {
  return (
    <StyledButton
      color={color}
      borderRadius={borderRadius}
      background={background}
    >
      {text}
    </StyledButton>
  );
};

const StyledButton = styled(Button)<IPButtonProps>`
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
  }
`;

export default PButton;
