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

interface ISelectProps {
  fileName: string;
  fileId: string;
  uploadTime: string;
}

export const CreateImageAsparameter = (
  data: IFilesCard[],
  imageParameters: string[],
  setImageParameters: Dispatch<SetStateAction<string[]>>,
): ReactNode => {
  const aa: IFilesCard[] = data;

  const onSelectImage = (value: ISelectProps) => {
    setImageParameters([value.fileId]);
  };

  return (
    <StyledImageWrapper>
      {aa.map((value: any) => (
        <StyledImage
          onClick={() => {
            onSelectImage(value);
          }}
        >
          {value.fileName}
          {/* <div>{value.fileId}</div>
         
          <div>{value.uploadTime}</div> */}
        </StyledImage>
      ))}
    </StyledImageWrapper>
  );
};

const StyledImageWrapper = styled.div`
  display: flex;
  gap: 10px;
`;

const StyledImage = styled.div`
  width: 120px;
  font-size: 12px;
  height: 120px;
  cursor: pointer;
  background-color: lightgray;
  border: 1px solid black;
  border-radius: 12px;
  padding: 20px;
`;
