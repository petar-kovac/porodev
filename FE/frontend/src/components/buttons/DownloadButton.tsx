import { DownloadOutlined } from '@ant-design/icons';
import { Button } from 'antd';
import { FC, MouseEventHandler } from 'react';
import styled from 'styled-components';
import theme from 'theme/theme';

interface IPButtonProps {
  color?: string;
  borderRadius?: string;
  background?: string;
  onClick?: MouseEventHandler<HTMLButtonElement> | undefined;
}

const DownloadButton: FC<IPButtonProps> = ({
  color,
  borderRadius,
  background,
  onClick,
}) => {
  return (
    <StyledDownloadButton
      color={color}
      borderRadius={borderRadius}
      background={background}
      onClick={onClick}
      type="primary"
      icon={<DownloadOutlined />}
    >
      Download
    </StyledDownloadButton>
  );
};

const StyledDownloadButton = styled(Button).attrs(() => ({
  'data-testid': 'download-button',
}))<IPButtonProps>`
  color: white;
  border-radius: 8px;
`;

export default DownloadButton;
