import axios from 'axios';
import { useFetchData } from 'hooks/useFetchData';
import {
  Dispatch,
  ReactNode,
  SetStateAction,
  useEffect,
  useState,
} from 'react';
import { IRuntimeRsponse } from 'service/runtime/runtime.props';
import {
  formatDate,
  millisToMinutesAndSeconds,
} from 'util/helpers/date-formaters';
import { IFilesCard } from 'types/card-data';
import { Image } from 'antd';
import styled from 'styled-components';
import { usePageContext } from 'context/PageContext';
import PList from 'components/list/List';
import UsersList from 'components/list/UsersList';

interface ISelectProps {
  fileName: string;
  fileId: string;
  uploadTime: string;
}

export const RenderUserList = (
  cardData: any,
  data: any,
  imageParameters: string[],
  setIsSiderModalVisible: Dispatch<SetStateAction<boolean>>,
): ReactNode => {
  return (
    <UsersList
      data={data}
      cardData={cardData}
      setIsSiderModalVisible={setIsSiderModalVisible}
    />
  );
};
