import { UserOutlined } from '@ant-design/icons';
import { Avatar, Card } from 'antd';
import React, { FC } from 'react';
import styled from 'styled-components';
import { StyledUserOutlined } from 'styles/icons/styled-icons';
import { StyledGroupCard, StyledTextWrapper } from './card-styled';

interface IGroupCardProps {
  groupName?: string;
  isModerator?: boolean;
  moderatorName?: string;
  numberOfFiles?: number;
  numberOfUsers?: number;
  onClick: React.MouseEventHandler<HTMLDivElement>;
  uuid: string;
}

const GroupCard: FC<IGroupCardProps> = ({
  groupName,
  isModerator,
  moderatorName,
  numberOfFiles,
  numberOfUsers,
  onClick,
  uuid,
}) => {
  return (
    <StyledGroupCard
      title={groupName}
      extra={isModerator && <StyledUserOutlined />}
      onClick={onClick}
      key={uuid}
    >
      <StyledTextWrapper>
        <div>Moderator:</div>
        <div>{moderatorName}</div>
      </StyledTextWrapper>
      <StyledTextWrapper>
        <div>Number of files:</div>
        <div>{numberOfFiles}</div>
      </StyledTextWrapper>
      <StyledTextWrapper>
        <div>Number of users:</div>
        <div>{numberOfUsers}</div>
      </StyledTextWrapper>
    </StyledGroupCard>
  );
};

export default GroupCard;
