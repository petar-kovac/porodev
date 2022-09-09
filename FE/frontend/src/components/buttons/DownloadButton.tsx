import { DownloadOutlined } from '@ant-design/icons';
import { Button } from 'antd';
import { FC, MouseEventHandler } from 'react';
import styled from 'styled-components';
import theme from 'theme/theme';

interface IPButtonProps {
  href?: string;
  download?: string;
  color?: string;
  borderRadius?: string;
  background?: string;
  onClick?: MouseEventHandler<HTMLButtonElement> | undefined;
  onClickCapture?: MouseEventHandler<HTMLButtonElement> | undefined;
}

const DownloadButton: FC<IPButtonProps> = ({
  href,
  download,
  color,
  borderRadius,
  background,
  onClick,
  onClickCapture,
}) => {
  return (
    <StyledDownloadButton
      href={href}
      download={download}
      color={color}
      borderRadius={borderRadius}
      background={background}
      onClick={onClick}
      onClickCapture={onClickCapture}
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
