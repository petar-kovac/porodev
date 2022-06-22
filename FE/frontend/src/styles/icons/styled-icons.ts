import styled from 'styled-components';
import {
  AppstoreOutlined,
  BarsOutlined,
  CloseOutlined,
  FieldTimeOutlined,
  FileImageOutlined,
  FolderFilled,
} from '@ant-design/icons';
import { Airplane } from '@styled-icons/ionicons-outline';
import { Spin } from 'antd';

export const StyledFolder = styled(FolderFilled)`
  color: ${({ theme: { colors } }) => colors.primary};
  font-size: 100px;
`;
export const StyledRuntimeIcon = styled(FieldTimeOutlined)`
  color: ${({ theme: { colors } }) => colors.primary};
  font-size: 100px;
`;
export const StyledFile = styled(FileImageOutlined)`
  color: ${({ theme: { colors } }) => colors.primary};
  font-size: 100px;
`;
export const StyledAppstoreOutlined = styled(AppstoreOutlined)`
  font-size: 2rem;
  cursor: pointer;
  color: #555;
`;

export const StyledBarsOutlined = styled(BarsOutlined)`
  font-size: 2rem;
  cursor: pointer;
  color: #555;
`;
export const StyledSpin = styled(Spin).attrs({
  'data-testid': 'spinner',
})<{ speed: number }>`
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

export const StyledIcon = styled(Airplane)<{ size: number; color: string }>`
  font-size: ${(props) => props.size}px;
  color: ${(props) => props.color};
`;
export const StyledClose = styled(CloseOutlined)`
  color: ${({ theme: { colors } }) => colors.primary};
`;
