import {
  HolderOutlined,
  LoadingOutlined,
  UserOutlined,
} from '@ant-design/icons';
import { Spin } from 'antd';
import { FC } from 'react';
import styled from 'styled-components';
import { Airplane } from '@styled-icons/ionicons-outline';

const Spinner: FC<{ size: number; color: string; speed: number }> = ({
  size,
  color,
  speed,
}) => {
  return (
    <div>
      <StyledSpin
        speed={speed}
        indicator={<StyledIcon color={color} size={size} />}
      />
    </div>
  );
};

export default Spinner;

const StyledSpin = styled(Spin)<{ speed: number }>`
  animation: rotation ${(props) => props.speed}s infinite linear;
  @keyframes rotation {
    from {
      transform: rotate(0deg);
    }
    to {
      transform: rotate(359deg);
    }
  }
`;

const StyledIcon = styled(Airplane)<{ size: number; color: string }>`
  font-size: ${(props) => props.size}px;
  color: ${(props) => props.color};
`;
